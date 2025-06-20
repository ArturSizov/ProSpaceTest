using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSpace.Api.Contracts.Request;
using ProSpace.Api.Contracts.Response;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Models;

namespace ProSpace.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<CustomersController> _logger;

        /// <summary>
        /// Customer service
        /// </summary>
        private readonly ICustomersService _service;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public CustomersController(ILogger<CustomersController> logger, ICustomersService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Get customers response
        /// </summary>
        /// <returns></returns>
        [HttpGet("/customers")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<List<CustomerResponse>>> GetAllCustomersAsync()
        {
            try
            {
                var response = await _service.ReadAllAsync();

                _logger.LogInformation($"{response?.Count()}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get customer by code response
        /// </summary>
        /// <returns></returns>
        [HttpGet("/customers/{email}")]
        [Authorize]
        public async Task<ActionResult<CustomerResponse>> GetCustomerByEmailAsync(string email)
        {
            try
            {
                var customer = await _service.GetByEmailAsync(email);

                if (customer == null)
                    return NotFound($"Client {email} not found");

                _logger.LogInformation($"{customer.Name}");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/create/customer")]
        [Authorize]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerRequest request)
        {
            try
            {
                var customer = new CustomerModel 
                { 
                    Name = request.Name,
                    Code = request.Code, 
                    Address = request.Address, 
                    Discount = request.Discount 
                };

                var result = await _service.CreateAsync(customer);

                if (result.Item1 == null)
                {
                    _logger.LogError("Failed to create customer");

                    return BadRequest(result.Item2);
                }

                _logger.LogInformation($"Customer created");

                return Ok(result.Item1);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("/update/customer/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateCustomerAsync(Guid id, [FromBody] CustomerRequest request)
        {
            try
            {
                var customer = new CustomerModel
                {
                    Id = id, 
                    Name = request.Name, 
                    Code = request.Code, 
                    Address = request.Address, 
                    Discount = request.Discount
                };

                var newCustomer = await _service.UpdateAsync(customer);

                if(newCustomer == null)
                {
                    _logger.LogError("Customer not updated");
                    return BadRequest("Customer not updated");
                } 

                _logger.LogInformation($"Customer updated");

                return Ok(newCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete customer 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("/delete/customer/{id:guid}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteCustomerAsync(Guid id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);

                if (!result)
                {
                    _logger.LogError("Failed to remove customer");
                    return BadRequest("Failed to remove customer");
                }

                _logger.LogInformation("Customer removed");

                return Ok("Customer removed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
