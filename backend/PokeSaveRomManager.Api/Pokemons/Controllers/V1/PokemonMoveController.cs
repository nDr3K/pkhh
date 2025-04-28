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
    [Route(ApiRoutes.Pokemon.Moves)]
    public class PokemonMoveController : Controller
    {
        private readonly IPokemonMoveService _pokemonMoveService;
        private readonly ILogger<PokemonMoveController> _logger;

        public PokemonMoveController(IPokemonMoveService pokemonMoveService, ILogger<PokemonMoveController> logger)
        {
            _pokemonMoveService = pokemonMoveService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PokemonMoveDto>>> GetAll(int pokemonId)
        {
            var moves = await _pokemonMoveService.GetAllAsync(pokemonId);
            _logger.LogInformation("Retrieved {Count} moves for Pokemon with ID {PokemonId}", moves.Count(), pokemonId);
            return Ok(moves);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PokemonMoveDto>> GetById(int id)
        {
            var move = await _pokemonMoveService.GetByIdAsync(id);
            if (move == null)
            {
                _logger.LogWarning("Move with ID {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Retrieved move with ID {Id}", id);
            return Ok(move);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<ActionResult<PokemonMoveDto>> Create(int pokemonId, PokemonMoveCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid move data provided");
                return BadRequest(ModelState);
            }

            var createdMove = await _pokemonMoveService.CreateAsync(pokemonId, dto);
            _logger.LogInformation("Created move with ID {Id} for Pokemon with ID {PokemonId}", createdMove.Id, pokemonId);
            return CreatedAtAction(nameof(GetById), new { id = createdMove.Id }, createdMove);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<IActionResult> Update(int id, int pokemonId, PokemonMoveUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid move data provided for update");
                return BadRequest(ModelState);
            }

            var exists = await _pokemonMoveService.ExistASync(id);
            if (!exists)
            {
                _logger.LogWarning("Move with ID {Id} not found for update", id);
                return NotFound();
            }

            await _pokemonMoveService.UpdateAsync(id, pokemonId, dto);
            _logger.LogInformation("Updated move with ID {Id} for Pokemon with ID {PokemonId}", id, pokemonId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _pokemonMoveService.ExistASync(id);
            if (!exists)
            {
                _logger.LogWarning("Move with ID {Id} not found for deletion", id);
                return NotFound();
            }

            await _pokemonMoveService.DeleteAsync(id);
            _logger.LogInformation("Deleted move with ID {Id}", id);
            return NoContent();
        }
    }
}
