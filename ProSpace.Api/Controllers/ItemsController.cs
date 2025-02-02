using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSpace.Api.Contracts;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Models;
using ProSpace.Domain.Services;

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
        //[Authorize]
        public async Task<ActionResult<List<ItemRequest>>> GetAllItemsAsync()
        {
            try
            {
                var response = await _itemsService.ReadAllAsync();

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
        /// Create item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/create/item")]
        public async Task<IActionResult> CreateItemAsync([FromBody] ItemRequest request)
        {
            try
            {
                var item = ItemModel.Create(Guid.NewGuid(), request.Code, request.Name, request.Price, request.Category);

                var result = await _itemsService.CreateAsync(item);

                if (!result)
                {
                    _logger.LogError("Failed to create product");

                    return BadRequest("Failed to create product");
                }

                _logger.LogInformation($"Product {request.Name} created");

                return Ok($"Product {request.Name} created");

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
        public async Task<IActionResult> UpdateItemAsync(Guid id, [FromBody] ItemRequest request)
        {
            try
            {
                var item = ItemModel.Create(id, request.Code, request.Name, request.Price, request.Name);

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

                _logger.LogInformation("Product removed");

                return Ok("Product removed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
