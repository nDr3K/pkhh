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
    [Route(ApiRoutes.Games.Types)]
    public class GameTypeController : Controller
    {
        private readonly IGameTypeService _gameTypeService;
        private readonly ILogger<GameTypeController> _logger;

        public GameTypeController(IGameTypeService gameTypeService, ILogger<GameTypeController> logger)
        {
            _gameTypeService = gameTypeService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameTypeDto>>> GetAllTypes()
        {
            _logger.LogInformation("Getting all game types");
            var types = await _gameTypeService.GetAllAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameTypeDto>> GetTypeById(int id)
        {
            _logger.LogInformation("Getting game type with id: {Id}", id);
            var type = await _gameTypeService.GetByIdAsync(id);
            if (type == null)
            {
                _logger.LogWarning("Game type with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(type);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GameTypeDto>> AddType(GameTypeCreateDto gameTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _logger.LogInformation("Adding new game type {GameTypeTypeId} for {GameTypeGameId}", gameTypeDto.TypeId, gameTypeDto.GameId);
            var gameType = await _gameTypeService.AddAsync(gameTypeDto);
            return CreatedAtAction(nameof(GetTypeById), new { id = gameType.Id }, gameType);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateType(int id, GameTypeUpdateDto gameTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingType = await _gameTypeService.GetByIdAsync(id);
            if (existingType == null)
            {
                _logger.LogWarning("Game type with id: {Id} not found", id);
                return NotFound();
            }

            await _gameTypeService.UpdateAsync(id, gameTypeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteType(int id)
        {
            var existingType = await _gameTypeService.GetByIdAsync(id);
            if (existingType == null)
            {
                _logger.LogWarning("Game type with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Deleting game type with id: {Id}", id);
            await _gameTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
