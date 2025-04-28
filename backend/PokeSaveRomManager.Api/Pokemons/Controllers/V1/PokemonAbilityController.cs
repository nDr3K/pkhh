using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Pokemons.DTOs;
using PokeSaveRomManager.Api.Pokemons.Services;
using PokeSaveRomManager.Api.Shared.Constants;
using PokeSaveRomManager.Api.Shared.Policies;

namespace PokeSaveRomManager.Api.Pokemons.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.Pokemon.Abilities)]
    public class PokemonAbilityController : Controller
    {
        private readonly IPokemonAbilityService _service;
        private readonly ILogger<PokemonAbilityController> _logger;

        public PokemonAbilityController(IPokemonAbilityService service, ILogger<PokemonAbilityController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PokemonAbilityDto>>> GetAll(int pokemonId)
        {
            var abilities = await _service.GetAllAsync(pokemonId);
            _logger.LogInformation("Fetched {Count} abilities for Pokemon with ID {PokemonId}", abilities.Count(), pokemonId);
            return Ok(abilities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PokemonAbilityDto>> GetById(int id)
        {
            var ability = await _service.GetByIdAsync(id);
            if (ability == null)
            {
                _logger.LogWarning("Ability with ID {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Fetched ability with ID {Id}", id);
            return Ok(ability);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<ActionResult<PokemonAbilityDto>> Create(int pokemonId, PokemonAbilityCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Received null DTO for creating ability");
                return BadRequest(ModelState);
            }

            var createdAbility = await _service.CreateAsync(pokemonId, dto);
            _logger.LogInformation("Created new ability with ID {Id} for Pokemon with ID {PokemonId}", createdAbility.Id, pokemonId);
            return CreatedAtAction(nameof(GetById), new { id = createdAbility.Id }, createdAbility);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<IActionResult> Update(int id, int pokemonId, PokemonAbilityUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Received null DTO for updating ability with ID {Id}", id);
                return BadRequest(ModelState);
            }

            if (!await _service.ExistsAsync(id))
            {
                _logger.LogWarning("Ability with ID {Id} not found", id);
                return NotFound();
            }

            await _service.UpdateAsync(id, pokemonId, dto);
            _logger.LogInformation("Updated ability with ID {Id} for Pokemon with ID {PokemonId}", id, pokemonId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _service.ExistsAsync(id))
            {
                _logger.LogWarning("Ability with ID {Id} not found", id);
                return NotFound();
            }

            await _service.DeleteAsync(id);
            _logger.LogInformation("Deleted ability with ID {Id}", id);
            return NoContent();
        }
    }
}
