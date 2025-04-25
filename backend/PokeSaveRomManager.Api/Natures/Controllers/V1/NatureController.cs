using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Natures.DTOs;
using PokeSaveRomManager.Api.Natures.Services;
using PokeSaveRomManager.Api.Shared.Constants;
using PokeSaveRomManager.Api.Shared.Policies;

namespace PokeSaveRomManager.Api.Natures.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.Natures.Root)]
    public class NatureController : Controller
    {
        private readonly INatureService _natureService;
        private readonly ILogger<NatureController> _logger;

        public NatureController(INatureService natureService, ILogger<NatureController> logger)
        {
            _natureService = natureService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NatureDto>>> GetNatures()
        {
            _logger.LogInformation("Getting all natures");
            var natures = await _natureService.GetAllAsync();
            return Ok(natures);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NatureDto>> GetNature(int id)
        {
            _logger.LogInformation("Getting nature with id: {Id}", id);
            var nature = await _natureService.GetByIdAsync(id);
            if (nature == null)
            {
                _logger.LogWarning("Nature with id: {Id} not found", id);
                return NotFound();
            }
            return Ok(nature);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.NaturesPolicy)]
        public async Task<ActionResult<NatureDto>> CreateNature(NatureCreateDto nature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating new nature: {NatureName}", nature.Name);
            var createdNature = await _natureService.AddAsync(nature);
            return CreatedAtAction(nameof(GetNature), new { id = createdNature.Id }, createdNature);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.NaturesPolicy)]
        public async Task<IActionResult> UpdateNature(int id, NatureUpdateDto nature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating nature with id: {Id}", id);
            var existingNature = await _natureService.GetByIdAsync(id);
            if (existingNature == null)
            {
                _logger.LogWarning("Nature with id: {Id} not found", id);
                return NotFound();
            }
            await _natureService.UpdateAsync(id, nature);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.NaturesPolicy)]
        public async Task<IActionResult> DeleteNature(int id)
        {
            _logger.LogInformation("Deleting nature with id: {Id}", id);
            var existingNature = await _natureService.GetByIdAsync(id);
            if (existingNature == null)
            {
                _logger.LogWarning("Nature with id: {Id} not found", id);
                return NotFound();
            }
            await _natureService.DeleteAsync(id);
            return NoContent();
        }
    }
}
