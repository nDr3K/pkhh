using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Items.DTOs;
using PokeSaveRomManager.Api.Items.Services;
using PokeSaveRomManager.Api.Shared.Constants;
using PokeSaveRomManager.Api.Shared.Policies;

namespace PokeSaveRomManager.Api.Items.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.Items.Root)]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAll()
        {
            _logger.LogInformation("Fetching all items");
            var items = await _itemService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemDto>> GetById(int id)
        {
            _logger.LogInformation($"Fetching item with ID: {id}");
            var item = await _itemService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.ItemsPolicy)]
        public async Task<ActionResult<ItemDto>> Create( ItemCreateDto itemCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exisitingItem = await _itemService.ExistsByNameAsync(itemCreateDto.Name);
            if (exisitingItem)
            {
                _logger.LogWarning($"Item with name {itemCreateDto.Name} already exists.");
                return BadRequest($"Item with name {itemCreateDto.Name} already exists.");
            }

            _logger.LogInformation("Creating new item");
            var createdItem = await _itemService.CreateAsync(itemCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.ItemsPolicy)]
        public async Task<IActionResult> Update(int id, ItemUpdateDto itemUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingItem = await _itemService.ExistsAsync(id);
            if (!existingItem)
            {
                _logger.LogWarning($"Item with ID: {id} not found for update.");
                return NotFound();
            }

            _logger.LogInformation($"Updating item with ID: {id}");
            await _itemService.UpdateAsync(id, itemUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.ItemsPolicy)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingItem = await _itemService.ExistsAsync(id);
            if (!existingItem)
            {
                _logger.LogWarning($"Item with ID: {id} not found for deletion.");
                return NotFound();
            }

            _logger.LogInformation($"Deleting item with ID: {id}");
            await _itemService.DeleteAsync(id);
            return NoContent();
        }
    }
}
