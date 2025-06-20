using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProSpace.Api.Contracts.Request;
using ProSpace.Api.Contracts.Response;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Interfaces.Validations;
using ProSpace.Domain.Models;
using ProSpace.Infrastructure.Entites.Supply;
using ProSpace.Infrastructure.Entites.Users;
using ProSpace.Infrastructure.Mappers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProSpace.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Asp user manager
        /// </summary>
        private UserManager<AppUser> _userManager;

        /// <summary>
        /// User role manager
        /// </summary>
        private RoleManager<AppRole> _roleManager;

        /// <summary>
        /// App configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Customer service
        /// </summary>
        private readonly ICustomersService _customersService;

        /// <summary>
        /// Customer validator
        /// </summary>
        private readonly IValidationProvider<CustomerModel> _validationCustomer;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userManager"></param>
        /// <param name="configuration"></param>
        /// <param name="customersService"></param>
        public UserController(ILogger<UserController> logger, UserManager<AppUser> userManager, IConfiguration configuration,
               ICustomersService customersService, IValidationProvider<CustomerModel> validationCustomer, RoleManager<AppRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
            _customersService = customersService;
            _validationCustomer = validationCustomer;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("/users")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<List<AppUser>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();

                _logger.LogInformation($"Users count: {users.Count.ToString()}");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Regustration user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("/register")]
        public async Task<IActionResult> UserRegisterAsync([FromBody] UserRequest request, string role = "customer")
        {
            try
            {
                var customer = new CustomerEntity
                {
                    Code = request.UserCode,
                    Name = request.UserName,
                    Address = request.Address,
                    Discount = request.Discount
                };

                var errors = new List<IdentityError>();

                var isValid = await _validationCustomer.ValidateAsync(customer.ToModel());

                if(!isValid.Item1 && isValid.Item2 != null)
                {
                    foreach (var error in isValid.Item2)
                    {
                        errors.Add(new IdentityError
                        {
                            Code = error.Key,
                            Description = error.Value[0]
                        });
                    }
                }

                if (errors.Count == 0)
                {
                    var user = new AppUser
                    {
                        Email = request.Email,
                        UserName = request.UserName,
                        EmailConfirmed = true,
                        Customer = customer
                    };

                    if(!await _roleManager.RoleExistsAsync(role))
                    {
                        _logger.LogError("Role does not exist");
                        return BadRequest("Role does not exist");
                    }

                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                        _logger.LogInformation($"User {user.Email} created");
                        return Ok(user.Id);
                    }

                    foreach (var error in result.Errors.ToList())
                        errors.Add(error);

                }

                _logger.LogError($"Failed to create user: {errors}");
                return BadRequest(errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Authentication user
        /// </summary>
        /// <param name="userResponse"></param>
        /// <returns></returns>
        [HttpPost("/login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] UserResponse userResponse)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userResponse.Email);

                if(user == null)
                {
                    _logger.LogError($"User with email address: {userResponse.Email} not found");
                    return NotFound($"User with email address: {userResponse.Email} not found");
                }

                if (await _userManager.CheckPasswordAsync(user, userResponse.Password))
                {
                    if(user.UserName != null)
                    {
                        var authClaims = new List<Claim>
                        {
                            new(ClaimTypes.Name, user.UserName),
                            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };

                        var userRoles = await _userManager.GetRolesAsync(user);

                        foreach (var role in userRoles)
                            authClaims.Add(new Claim(ClaimTypes.Role, role));

                        var jwtToken = GetToken(authClaims);

                        var customer = await _customersService.GetByEmailAsync(userResponse.Email);

                        var result = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                            expiration = jwtToken.ValidTo,
                            customer,
                            roles = userRoles
                        };

                        _logger.LogInformation($"result: { result.token }");

                        return Ok(result);
                    }
                }

                _logger.LogError("User is not authorized");
                return Unauthorized("Invalid password"); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpDelete("/delete")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteUserAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if(user == null)
                {
                    _logger.LogError($"User {email} not found");
                    return NotFound($"User {email} not found");
                }

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    var resultCustomer = await _customersService.DeleteAsync(user.CustomerId);

                    if (resultCustomer)
                    {
                        _logger.LogInformation($"User with email address: {user.Email} deleted");
                        return Ok($"User with email address: {user.Email} deleted");
                    }

                    _logger.LogError("Error deleting customer");
                    return BadRequest("Error deleting customer");
                }

                _logger.LogError("Error deleting customer");
                return BadRequest(result.Errors);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }          
        }

        /// <summary>
        /// Updated user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        [HttpPut("/update")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(string email, [FromBody] UserUpdateResponse response)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    _logger.LogError($"User {email} not found");
                    return NotFound($"User {email} not found");
                }

                var customer = await _customersService.ReadAsync(user.CustomerId);

                if (customer == null)
                {
                    _logger.LogError("Customer not found");
                    return NotFound("Customer not found");
                }

                user.Customer = customer.ToEntity();

                user.UserName = response.UserName;
                user.Email = response.Email;
                user.Customer.Name = response.UserName;
                user.Customer.Code = response.UserCode;
                user.Customer.Address = response.Address;
                user.Customer.Discount = response.Discount;

                var errors = new List<IdentityError>();

                var isValid = await _validationCustomer.ValidateAsync(user.Customer.ToModel());

                if (!isValid.Item1 && isValid.Item2 != null)
                {
                    foreach (var error in isValid.Item2)
                    {
                        errors.Add(new IdentityError
                        {
                            Code = error.Key,
                            Description = error.Value[0]
                        });
                    }
                }

                if (errors.Count == 0)
                {
                    var result = await _userManager.UpdateAsync(user);

                    foreach (var error in result.Errors.ToList())
                        errors.Add(error);

                    _logger.LogInformation("User updated");
                    return Ok($"User with email address: {user.UserName} updated");
                }

                return BadRequest(errors);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// User reset password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        [HttpPut("/reset-password")]
        [Authorize]
        public async Task<IActionResult> ResetUserPasswordAsync(string email, [FromBody] UpdateUserPasswordResponse response)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    return NotFound($"User {email} not found");

                if (await _userManager.CheckPasswordAsync(user, response.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var result = await _userManager.ResetPasswordAsync(user, token, response.NewPassword);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"User {user.Email} password has been updated");
                        return Ok($"User {user.Email} password has been updated");
                    }

                    _logger.LogError("Error updated user password");
                    return BadRequest(result.Errors);
                }

                return BadRequest("The password you entered does not match");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            } 
        }

        /// <summary>
        /// Get user token
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ??                                                                      throw new Exception("JWT secret not found")));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
