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
    [Route(ApiRoutes.Pokemon.Root)]
    public class PokemonController : Controller
    {
        private readonly IPokemonService _pokemonService;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IPokemonService pokemonService, ILogger<PokemonController> logger)
        {
            _pokemonService = pokemonService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PokemonDto>>> GetAllAsync()
        {
            _logger.LogInformation("Getting all Pokemons");
            var pokemons = await _pokemonService.GetAllAsync();
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PokemonDto>> GetByIdAsync(int id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);
            if (pokemon == null)
            {
                _logger.LogWarning("Pokemon with id {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Getting Pokemon with id {Id}", id);
            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<ActionResult<PokemonDto>> CreateAsync(PokemonCreateDto pokemon)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for Pokemon creation");
                return BadRequest(ModelState);
            }

            var createdPokemon = await _pokemonService.CreateAsync(pokemon);
            _logger.LogInformation("Pokemon created with id {Id}", createdPokemon.Id);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdPokemon.Id }, createdPokemon);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<IActionResult> UpdateAsync(int id, PokemonUpdateDto pokemon)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for Pokemon update");
                return BadRequest(ModelState);
            }

            var exists = await _pokemonService.ExistAsync(id);
            if (!exists)
            {
                _logger.LogWarning("Pokemon with id {Id} not found", id);
                return NotFound();
            }

            await _pokemonService.UpdateAsync(id, pokemon);
            _logger.LogInformation("Pokemon with id {Id} updated", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var exists = await _pokemonService.ExistAsync(id);
            if (!exists)
            {
                _logger.LogWarning("Pokemon with id {Id} not found", id);
                return NotFound();
            }

            await _pokemonService.DeleteAsync(id);
            _logger.LogInformation("Pokemon with id {Id} deleted", id);
            return NoContent();
        }
    }
}
