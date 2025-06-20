using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProSpace.Api.Contracts.Request;
using ProSpace.Api.Contracts.Response;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Models;

namespace ProSpace.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ItemsController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<ItemsController> _logger;

        /// <summary>
        /// Items service
        /// </summary>
        private readonly IItemsService _itemsService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="itemsService"></param>
        public ItemsController(ILogger<ItemsController> logger, IItemsService itemsService)
        {
            _logger = logger;
            _itemsService = itemsService;
        }

        /// <summary>
        /// Get items response
        /// </summary>
        /// <returns></returns>
        [HttpGet("/items")]
        public async Task<ActionResult<List<ItemResponse>>> GetAllItemsAsync()
        {
            try
            {
                var response = await _itemsService.ReadAllAsync();

                _logger.LogInformation(response?.Length.ToString());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Create item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/create/item")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateItemAsync([FromBody] ItemRequest request)
        {
            try
            {
                var item = new ItemModel
                {
                    Category = request.Category,
                    Code = request.Code,
                    Name = request.Name,
                    Price = request.Price
                };

                var result = await _itemsService.CreateAsync(item);

                if (result.Item1 == null)
                {
                    _logger.LogError("Failed to create product");

                    return BadRequest(result.Item2);
                }

                _logger.LogInformation("Product {request.Name} created", request.Name);

                return Ok(result.Item1);

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
        [HttpPut("/update/item/{id:guid}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateItemAsync(Guid id, [FromBody] ItemRequest request)
        {
            try
            {
                var item = new ItemModel
                {
                    Id = id,
                    Code = request.Code, 
                    Name = request.Name,
                    Price = request.Price, 
                    Category = request.Category
                };

                var result = await _itemsService.UpdateAsync(item);

                _logger.LogInformation($"Product updated");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete item 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("/delete/item/{id:guid}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteItemAsync(Guid id)
        {
            try
            {
                var result = await _itemsService.DeleteAsync(id);

                if (!result)
                {
                    _logger.LogError("Failed to remove product");
                    return BadRequest("Failed to remove product");
                }

                _logger.LogInformation($"Product removed: {id}");

                return Ok($"Product removed: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
