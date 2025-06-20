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
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<OrdersController> _logger;

        /// <summary>
        /// Order service
        /// </summary>
        private readonly IOrderService _service;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public OrdersController(ILogger<OrdersController> logger, IOrderService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Get orders response
        /// </summary>
        /// <returns></returns>
        [HttpGet("/orders")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<List<OrderResponse>>> GetAllOrdersAsync()
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
        /// Receives orders by customer ID controller
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("/orders/{customerId:guid}")]
        [Authorize]
        public async Task<ActionResult<OrderRequest[]>> GetByCustomerId(Guid customerId)
        {
            try
            {
                var orders = await _service.GetByCustomerId(customerId);

                _logger.LogInformation($"{orders?.Count()}");

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Receives orders by customer code
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        [HttpPut("/orders/{customerCode}")]
        [Authorize]
        public async Task<ActionResult<OrderRequest[]>> GetByCustomerCodeAsync(string customerCode)
        {
            try
            {
                var orders = await _service.GetByCustomerCodeAsync(customerCode);

                _logger.LogInformation(orders?.Count().ToString());

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Receives an order by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpPut("/orders/{orderNumber:int}")]
        [Authorize]
        public async Task<ActionResult<OrderRequest>> GetByOrderNumber(int orderNumber)
        {
            try
            {
                var order = await _service.GetByOrderNumber(orderNumber);

                if(order == null)
                {
                    _logger.LogError("Order by number: {orderNumber} not found", orderNumber);
                    return NotFound($"Order by number: {orderNumber} not found");
                }

                _logger.LogInformation(order?.Id.ToString());

                return Ok(order);
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
        [HttpPost("/create/order")]
        [Authorize]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderRequest request)
        {
            try
            {
                var order = new OrderModel
                {
                    CustomerId = request.CustomerId, 
                    OrderDate = request.OrderDate, 
                    ShipmentDate = request.ShipmentDate, 
                    OrderNumber = request.OrderNumber, 
                    Status = request.Status 
                };

                var result = await _service.CreateAsync(order);

                if (result.Item1 == null)
                {
                    _logger.LogError("Failed to create order");

                    return BadRequest(result.Item2);
                }

                _logger.LogInformation($"Order created");

                return Ok(result.Item1);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update order reques
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("/update/order/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateOrderItemAsync(Guid id, [FromBody] OrderRequest request)
        {
            try
            {
                var order = new OrderModel
                {
                    Id = id, 
                    CustomerId = request.CustomerId, 
                    OrderDate = request.OrderDate, 
                    ShipmentDate = request.ShipmentDate, 
                    OrderNumber = request.OrderNumber, 
                    Status = request.Status 
                };

                var result = await _service.UpdateAsync(order);

                _logger.LogInformation($"Order updated");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete order 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("/delete/order/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrderItemAsync(Guid id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);

                if (!result)
                {
                    _logger.LogError("Failed to remove order");
                    return BadRequest("Failed to remove order");
                }

                _logger.LogInformation("Order removed");

                return Ok("Order removed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
