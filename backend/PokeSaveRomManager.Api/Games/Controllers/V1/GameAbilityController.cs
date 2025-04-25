using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Games.Services;
using PokeSaveRomManager.Api.Shared.Constants;
using PokeSaveRomManager.Api.Shared.Policies;

namespace PokeSaveRomManager.Api.Games.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.Games.Abilities)]
    public class GameAbilityController : Controller
    {
        private readonly IGameAbilityService _gameAbilityService;
        private readonly ILogger<GameAbilityController> _logger;

        public GameAbilityController(IGameAbilityService gameAbilityService, ILogger<GameAbilityController> logger)
        {
            _gameAbilityService = gameAbilityService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int gameId)
        {
            _logger.LogInformation("Retrieving all abilities for game with ID: {gameId}", gameId);
            var gameAbilities = await _gameAbilityService.GetAllAsync(gameId);
            return Ok(gameAbilities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int gameId, int id)
        {
            _logger.LogInformation("Retrieving ability with ID: {Id} for game with ID: {gameId}", id, gameId);
            var gameAbility = await _gameAbilityService.GetByIdAsync(gameId, id);
            if (gameAbility == null)
            {
                return NotFound();
            }

            return Ok(gameAbility);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.GamesPolicy)]
        public async Task<IActionResult> Add(int gameId, GameAbilityCreateDto gameAbilityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _logger.LogInformation("Adding new ability with ID: {AbilityId} for game with ID: {GameId}", gameAbilityDto.AbilityId, gameId);
            var addedGameAbility = await _gameAbilityService.AddAsync(gameId, gameAbilityDto);
            return CreatedAtAction(nameof(GetById), new { id = addedGameAbility.Id }, addedGameAbility);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.GamesPolicy)]
        public async Task<IActionResult> Update(int gameId, int id, GameAbilityUpdateDto gameAbilityDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingAbility = await _gameAbilityService.GetByIdAsync(gameId, id);
            if (existingAbility == null)
            {
                _logger.LogWarning("Ability with ID: {Id} not found for game with ID: {gameId}", id, gameId);
                return NotFound();
            }

            _logger.LogInformation("Updating game ability with ID: {Id} for game with ID: {gameId}", id, gameId);
            await _gameAbilityService.UpdateAsync(gameId, id, gameAbilityDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.GamesPolicy)]
        public async Task<IActionResult> Delete(int gameId, int id)
        {
            var existingAbility = await _gameAbilityService.GetByIdAsync(gameId, id);
            if (existingAbility == null)
            {
                _logger.LogWarning("Game ability with ID: {Id} not found for game with ID: {gameId}", id, gameId);
                return NotFound();
            }

            _logger.LogInformation("Deleting game ability with ID: {Id} for game with ID: {gameId}", id, gameId);
            await _gameAbilityService.DeleteAsync(gameId, id);
            return NoContent();
        }
    }
}
