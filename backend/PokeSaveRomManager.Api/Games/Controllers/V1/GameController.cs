using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Games.Services;
using PokeSaveRomManager.Api.Shared.Policies;

namespace PokeSaveRomManager.Api.Games.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/games")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GameController> _logger;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
        {
            _logger.LogInformation("Getting all games");
            var games = await _gameService.GetAllGamesAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            _logger.LogInformation("Getting game with id: {Id}", id);
            var game = await _gameService.GetGameByIdAsync(id);

            if (game == null)
            {
                _logger.LogWarning("Game with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(game);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.GamesPolicy)]
        public async Task<ActionResult<GameDto>> CreateGame(GameCreateDto game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new game");
            var createdGame = await _gameService.AddGameAsync(game);

            return CreatedAtAction(nameof(GetGame), new { id = createdGame.Id, version = HttpContext.GetRequestedApiVersion().ToString() }, createdGame);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.GamesPolicy)]
        public async Task<IActionResult> UpdateGame(int id, GameUpdateDto game)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating game with id: {Id}", id);
            var exists = await _gameService.GameExistsAsync(id);

            if (!exists)
            {
                _logger.LogWarning("Game with id: {Id} not found for update", id);
                return NotFound();
            }

            await _gameService.UpdateGameAsync(id, game);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.GamesPolicy)]
        public async Task<IActionResult> DeleteGame(int id)
        {
            _logger.LogInformation("Deleting game with id: {Id}", id);
            var exists = await _gameService.GameExistsAsync(id);

            if (!exists)
            {
                _logger.LogWarning("Game with id: {Id} not found for deletion", id);
                return NotFound();
            }

            await _gameService.DeleteGameAsync(id);
            return NoContent();
        }

        [HttpGet("generation/{generation}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByGeneration(int generation)
        {
            _logger.LogInformation("Getting games for generation: {Generation}", generation);
            var games = await _gameService.GetGamesByGenerationAsync(generation);
            return Ok(games);
        }

        [HttpGet("region/{region}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByRegion(string region)
        {
            _logger.LogInformation("Getting games for region: {Region}", region);
            var games = await _gameService.GetGamesByRegionAsync(region);
            return Ok(games);
        }

        [HttpGet("official")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetOfficialGames()
        {
            _logger.LogInformation("Getting official games");
            var games = await _gameService.GetOfficialGamesAsync();
            return Ok(games);
        }
    }
}
