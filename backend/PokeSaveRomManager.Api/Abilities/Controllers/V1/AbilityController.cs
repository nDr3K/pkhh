using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Abilities.DTOs;
using PokeSaveRomManager.Api.Abilities.Services;
using PokeSaveRomManager.Api.Shared.Policies;

namespace PokeSaveRomManager.Api.Abilities.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/abilities")]
    public class AbilityController: Controller
    {
        private readonly IAbilityService _abilityService;
        private readonly ILogger<AbilityController> _logger;

        public AbilityController(IAbilityService abilityService, ILogger<AbilityController> logger)
        {
            _abilityService = abilityService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AbilityDto>>> GetAbilities()
        {
            _logger.LogInformation("Getting all abilities");
            var abilities = await _abilityService.GetAllAsync();
            return Ok(abilities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AbilityDto>> GetAbility(int id)
        {
            _logger.LogInformation("Getting ability with id: {Id}", id);
            var ability = await _abilityService.GetByIdAsync(id);
            if (ability == null)
            {
                _logger.LogWarning("Ability with id: {Id} not found", id);
                return NotFound();
            }
            return Ok(ability);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.AbilitiesPolicy)]
        public async Task<ActionResult<AbilityDto>> CreateAbility(AbilityCreateDto ability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alreadyExists = await _abilityService.ExistsByNameAsync(ability.Name);
            if (alreadyExists)
            {
                _logger.LogWarning("Ability with name: {AbilityName} already exists", ability.Name);
                return BadRequest($"Ability with name {ability.Name} already exists.");
            }

            _logger.LogInformation("Creating new ability: {AbilityName}", ability.Name);
            var createdAbility = await _abilityService.CreateAsync(ability);
            return CreatedAtAction(nameof(GetAbility), new { id = createdAbility.Id }, createdAbility);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.AbilitiesPolicy)]
        public async Task<IActionResult> UpdateAbility(int id, AbilityUpdateDto ability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating ability with id: {Id}", id);
            await _abilityService.UpdateAsync(id, ability);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.AbilitiesPolicy)]
        public async Task<IActionResult> DeleteAbility(int id)
        {
            _logger.LogInformation("Deleting ability with id: {Id}", id);
            var exists = await _abilityService.ExistsAsync(id);
            if (!exists)
            {
                _logger.LogWarning("Ability with id: {Id} not found", id);
                return NotFound();
            }
            await _abilityService.DeleteAsync(id);
            return NoContent();
        }
    }
}
