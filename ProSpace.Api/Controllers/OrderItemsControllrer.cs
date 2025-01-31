using Microsoft.AspNetCore.Mvc;
using ProSpace.Api.Contracts;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Models;
using ProSpace.Domain.Services;

namespace ProSpace.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderItemsControllrer : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<OrderItemsControllrer> _logger;

        /// <summary>
        /// Order items service
        /// </summary>
        private readonly IOtderItemsService _service;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public OrderItemsControllrer(ILogger<OrderItemsControllrer> logger, IOtderItemsService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Get order items response
        /// </summary>
        /// <returns></returns>
        [HttpGet("/orderitems")]
        //[Authorize]
        public async Task<ActionResult<List<OrderItemRequest>>> GetAllOrderItemsAsync()
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
        /// Create order item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/create/order-item")]
        public async Task<IActionResult> CreateOrderItemAsync([FromBody] OrderItemRequest request)
        {
            try
            {
                var orderItem = OrderItemModel.Create(Guid.NewGuid(), request.OrderId, request.ItemId, request.ItemsCount, request.ItemPrice);

                var result = await _service.CreateAsync(orderItem);

                if (!result)
                {
                    _logger.LogError("Failed to create order item");

                    return BadRequest("Failed to create order item");
                }

                _logger.LogInformation($"Order item created");

                return Ok($"Order item created");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update item reques
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("/update/order-item/{id:guid}")]
        public async Task<IActionResult> UpdateOrderItemAsync(Guid id, [FromBody] OrderItemRequest request)
        {
            try
            {
                var orderItem = OrderItemModel.Create(id, request.OrderId, request.ItemId, request.ItemsCount, request.ItemPrice);

                var result = await _service.UpdateAsync(orderItem);

                _logger.LogInformation($"Order item updated");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete order item 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("/delete/item-orders/{id:guid}")]
        public async Task<IActionResult> DeleteOrderItemAsync(Guid id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);

                if (!result)
                {
                    _logger.LogError("Failed to remove order item");
                    return BadRequest("Failed to remove order item");
                }

                _logger.LogInformation("Order item removed");

                return Ok("Order item removed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
