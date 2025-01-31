using Microsoft.AspNetCore.Mvc;
using ProSpace.Api.Contracts;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Models;

namespace ProSpace.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<CustomerController> _logger;

        /// <summary>
        /// Customer service
        /// </summary>
        private readonly ICustomersService _service;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public CustomerController(ILogger<CustomerController> logger, ICustomersService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Get orders response
        /// </summary>
        /// <returns></returns>
        [HttpGet("/customers")]
        //[Authorize]
        public async Task<ActionResult<List<CustomerRequest>>> GetAllCustomersAsync()
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
        /// Create order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/create/customer")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CustomerRequest request)
        {
            try
            {
                var customer = CustomerModel.Create(Guid.NewGuid(), request.Name, request.Code, request.Address, request.Discount);

                var result = await _service.CreateAsync(customer);

                if (!result)
                {
                    _logger.LogError("Failed to create customer");

                    return BadRequest("Failed to create customer");
                }

                _logger.LogInformation($"Customer created");

                return Ok($"Customer created");

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
        public async Task<IActionResult> UpdateCustomerAsync(Guid id, [FromBody] CustomerRequest request)
        {
            try
            {
                var customer = CustomerModel.Create(id, request.Name, request.Code, request.Address, request.Discount);

                var result = await _service.UpdateAsync(customer);

                _logger.LogInformation($"Customer updated");

                return Ok(result);
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
        [HttpDelete("/delete/customers/{id:guid}")]
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
