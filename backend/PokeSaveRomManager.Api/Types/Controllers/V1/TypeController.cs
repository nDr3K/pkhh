using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Shared.Policies;
using PokeSaveRomManager.Api.Types.DTOs;
using PokeSaveRomManager.Api.Types.Services;

namespace PokeSaveRomManager.Api.Types.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/types")]
    public class TypeController : Controller
    {
        private readonly ITypeService _typeService;
        private readonly ILogger<TypeController> _logger;

        public TypeController(ITypeService typeService, ILogger<TypeController> logger)
        {
            _typeService = typeService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            _logger.LogInformation("Getting all types");
            var types = await _typeService.GetAllAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeDto>> GetType(int id)
        {
            _logger.LogInformation("Getting type with id: {Id}", id);
            var type = await _typeService.GetByIdAsync(id);
            if (type == null)
            {
                _logger.LogWarning("Type with id: {Id} not found", id);
                return NotFound();
            }
            return Ok(type);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.TypesPolicy)]
        public async Task<ActionResult<TypeDto>> CreateType(TypeCreateDto type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating new type: {TypeName}", type.Name);
            var createdType = await _typeService.AddAsync(type);
            return CreatedAtAction(nameof(GetType), new { id = createdType.Id }, createdType);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.TypesPolicy)]
        public async Task<IActionResult> UpdateType(int id, TypeUpdateDto type)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating type with id: {Id}", id);
            var exists = await _typeService.ExistsAsync(id);

            if (!exists)
            {
                _logger.LogWarning("Type with id: {Id} not found", id);
                return NotFound();
            }

            await _typeService.UpdateAsync(id, type);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.TypesPolicy)]
        public async Task<IActionResult> DeleteType(int id)
        {
            var exists = await _typeService.ExistsAsync(id);
            if (!exists)
            {
                _logger.LogWarning("Type with id: {Id} not found", id);
                return NotFound();
            }
            await _typeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
