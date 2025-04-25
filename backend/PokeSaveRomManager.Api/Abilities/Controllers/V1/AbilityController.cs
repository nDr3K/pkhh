using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Abilities.DTOs;
using PokeSaveRomManager.Api.Abilities.Services;
using PokeSaveRomManager.Api.Shared.Constants;
using PokeSaveRomManager.Api.Shared.Policies;

namespace PokeSaveRomManager.Api.Abilities.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.Abilities.Root)]
    public class AbilityController : Controller
    {
        private readonly IAbilityService _abilityService;
        private readonly ILogger<AbilityController> _logger;

        public AbilityController(IAbilityService abilityService, ILogger<AbilityController> logger)
        {
            _abilityService = abilityService;
            _logger = logger;
        }

        //Ability
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

            _logger.LogInformation("Creating new ability with name id: {NameId}", ability.NameId);
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

        //AbilityName
        [HttpGet(ApiRoutes.Abilities.Names)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AbilityNameDto>>> GetAbilityNames()
        {
            _logger.LogInformation("Getting all ability names");
            var abilityNames = await _abilityService.GetAllNamesAsync();
            return Ok(abilityNames);
        }

        [HttpGet(ApiRoutes.Abilities.Names + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AbilityNameDto>> GetAbilityName(int id)
        {
            _logger.LogInformation("Getting ability name with id: {Id}", id);
            var abilityName = await _abilityService.GetNameByIdAsync(id);
            if (abilityName == null)
            {
                _logger.LogWarning("Ability name with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(abilityName);
        }

        [HttpPost(ApiRoutes.Abilities.Names)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.AbilitiesPolicy)]
        public async Task<ActionResult<AbilityNameDto>> CreateAbilityName(AbilityNameCreateDto abilityName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating new ability: {Name}", abilityName.Name);
            var createdAbilityName = await _abilityService.CreateNameAsync(abilityName);
            return CreatedAtAction(nameof(GetAbilityName), new { id = createdAbilityName.Id }, createdAbilityName);
        }

        [HttpPut(ApiRoutes.Abilities.Names + "/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.AbilitiesPolicy)]
        public async Task<IActionResult> UpdateAbilityName(int id, AbilityNameUpdateDto abilityName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating ability name with id: {Id}", id);
            await _abilityService.UpdateNameAsync(id, abilityName);
            return NoContent();
        }

        [HttpDelete(ApiRoutes.Abilities.Names + "/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.AbilitiesPolicy)]
        public async Task<IActionResult> DeleteAbilityName(int id)
        {
            _logger.LogInformation("Deleting ability name with id: {Id}", id);
            var exists = await _abilityService.ExistsNameAsync(id);
            if (!exists)
            {
                _logger.LogWarning("Ability name with id: {Id} not found", id);
                return NotFound();
            }

            await _abilityService.DeleteNameAsync(id);
            return NoContent();
        }
    }
}
