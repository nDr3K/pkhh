using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Games.Mapper;
using PokeSaveRomManager.Api.Games.Repositories;

namespace PokeSaveRomManager.Api.Games.Services
{
    public class GameService: IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all games");
                var games = await _gameRepository.GetAllAsync();
                return games.ToDtos(); // GameMapper Collection Method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all games");
                throw;
            }
        }

        public async Task<GameDto> GetGameByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving game with ID: {Id}", id);
                var game = await _gameRepository.GetByIdWithRelatedEntitiesAsync(id);
                return GameMapper.ToDto(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving game with ID: {Id}", id);
                throw;
            }
        }

        public async Task<GameDto> AddGameAsync(GameCreateDto gameDto)
        {
            try
            {
                _logger.LogInformation("Adding new game: {GameName}", gameDto.Name);
                var game = GameMapper.ToEntity(gameDto);
                var addedGame = await _gameRepository.AddAsync(game);
                return GameMapper.ToDto(addedGame);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding game: {GameName}", gameDto.Name);
                throw;
            }
        }

        public async Task UpdateGameAsync(int id, GameUpdateDto gameDto)
        {
            try
            {
                var game = await _gameRepository.GetByIdAsync(id);
                game.UpdateFromDto(gameDto);
                _logger.LogInformation("Updating game with ID: {Id}", game.Id);
                await _gameRepository.UpdateAsync(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating game with ID: {Id}", id);
                throw;
            }
        }

        public async Task DeleteGameAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting game with ID: {Id}", id);
                await _gameRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting game with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> GameExistsAsync(int id)
        {
            try
            {
                return await _gameRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if game exists with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<GameDto>> GetGamesByGenerationAsync(int generation)
        {
            try
            {
                _logger.LogInformation("Retrieving games for generation: {Generation}", generation);
                var games = await _gameRepository.GetByGenerationAsync(generation);
                return games.ToDtos(); // GameMapper Collection Method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving games for generation: {Generation}", generation);
                throw;
            }
        }

        public async Task<IEnumerable<GameDto>> GetGamesByRegionAsync(string region)
        {
            try
            {
                _logger.LogInformation("Retrieving games for region: {Region}", region);
                var games = await _gameRepository.GetByRegionAsync(region);
                return games.ToDtos(); // GameMapper Collection Method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving games for region: {Region}", region);
                throw;
            }
        }

        public async Task<IEnumerable<GameDto>> GetOfficialGamesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving official games");
                var games = await _gameRepository.GetOfficialAsync();
                return games.ToDtos(); // GameMapper Collection Method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving official games");
                throw;
            }
        }
    }
}
