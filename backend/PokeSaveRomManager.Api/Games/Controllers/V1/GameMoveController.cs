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
    [Route(ApiRoutes.Games.Moves)]
    public class GameMoveController : Controller
    {
        private readonly IGameMoveService _gameMoveService;
        private readonly ILogger<GameMoveController> _logger;

        public GameMoveController(IGameMoveService gameMoveService, ILogger<GameMoveController> logger)
        {
            _gameMoveService = gameMoveService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Retrieving all game moves");
            var gameMoves = await _gameMoveService.GetAllAsync();
            return Ok(gameMoves);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var gameMove = await _gameMoveService.GetByIdAsync(id);
            if (gameMove == null)
            {
                _logger.LogWarning("Game move with ID: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Retrieving game move with ID: {Id}", id);
            return Ok(gameMove);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.GamesPolicy)]
        public async Task<IActionResult> Add(GameMoveCreateDto gameMoveDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _logger.LogInformation("Adding new game move {GameMoveId} for {GameId}", gameMoveDto.MoveId, gameMoveDto.GameId);
            var gameMove = await _gameMoveService.AddAsync(gameMoveDto);
            return CreatedAtAction(nameof(GetById), new { id = gameMove.Id }, gameMove);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, GameMoveUpdateDto gameMoveDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingGameMove = await _gameMoveService.GetByIdAsync(id);
            if (existingGameMove == null)
            {
                _logger.LogWarning("Game move with ID: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating game move with ID: {Id}", id);
            await _gameMoveService.UpdateAsync(id, gameMoveDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.GamesPolicy)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingGameMove = await _gameMoveService.GetByIdAsync(id);
            if (existingGameMove == null)
            {
                _logger.LogWarning("Game move with ID: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Deleting game move with ID: {Id}", id);
            await _gameMoveService.DeleteAsync(id);
            return NoContent();
        }
    }
}
