using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Games.Services;
using PokeSaveRomManager.Api.Shared.Constants;

namespace PokeSaveRomManager.Api.Games.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.Games.Root + "/{gameId}")]
    public class GameTypeController : Controller
    {
        private readonly IGameTypeService _gameTypeService;
        private readonly ILogger<GameTypeController> _logger;

        public GameTypeController(IGameTypeService gameTypeService, ILogger<GameTypeController> logger)
        {
            _gameTypeService = gameTypeService;
            _logger = logger;
        }

        [HttpGet(ApiRoutes.Games.Types)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameTypeDto>>> GetAllTypes(int gameId)
        {
            _logger.LogInformation("Getting all types for game with id: {Id}", gameId);
            var types = await _gameTypeService.GetAllAsync();
            return Ok(types);
        }

        [HttpGet(ApiRoutes.Games.Types + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameTypeDto>> GetTypeById(int gameId, int id)
        {
            _logger.LogInformation("Getting type with id: {Id} for game with id: {GameId}", id, gameId);
            var type = await _gameTypeService.GetByIdAsync(id);
            if (type == null)
            {
                _logger.LogWarning("Type with id: {Id} not found for game with id: {GameId}", id, gameId);
                return NotFound();
            }

            return Ok(type);
        }

        [HttpPost(ApiRoutes.Games.Types)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GameTypeDto>> AddType(int gameId, GameTypeCreateDto gameTypeDto)
        {
            _logger.LogInformation("Adding new type for game with id: {Id}", gameId);
            if (gameTypeDto == null)
            {
                _logger.LogWarning("Game type DTO is null");
                return BadRequest();
            }

            var gameType = await _gameTypeService.AddAsync(gameTypeDto);
            return CreatedAtAction(nameof(GetTypeById), new { id = gameType.Id }, gameType);
        }

        [HttpPut(ApiRoutes.Games.Types + "/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateType(int gameId, int id, GameTypeUpdateDto gameTypeDto)
        {
            _logger.LogInformation("Updating type with id: {Id} for game with id: {GameId}", id, gameId);
            if (gameTypeDto == null)
            {
                _logger.LogWarning("Game type DTO is null");
                return BadRequest();
            }

            var existingType = await _gameTypeService.GetByIdAsync(id);
            if (existingType == null)
            {
                _logger.LogWarning("Type with id: {Id} not found for game with id: {GameId}", id, gameId);
                return NotFound();
            }

            await _gameTypeService.UpdateAsync(id, gameTypeDto);
            return NoContent();
        }

        [HttpDelete(ApiRoutes.Games.Types + "/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteType(int gameId, int id)
        {
            _logger.LogInformation("Deleting type with id: {Id} for game with id: {GameId}", id, gameId);
            var existingType = await _gameTypeService.GetByIdAsync(id);
            if (existingType == null)
            {
                _logger.LogWarning("Type with id: {Id} not found for game with id: {GameId}", id, gameId);
                return NotFound();
            }

            await _gameTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
