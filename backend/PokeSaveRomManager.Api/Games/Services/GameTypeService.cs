using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Games.Mapper;
using PokeSaveRomManager.Api.Games.Repositories;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Services
{
    public class GameTypeService : IGameTypeService
    {
        private readonly IGameTypeRepository _gameTypeRepository;
        private readonly ILogger<GameTypeService> _logger;

        public GameTypeService(IGameTypeRepository gameTypeRepository, ILogger<GameTypeService> logger)
        {
            _gameTypeRepository = gameTypeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<GameTypeDto>> GetAllAsync(int gameId)
        {
            try
            {
                _logger.LogInformation("Retrieving all game types for game with ID: {GameId}", gameId);
                var gameTypes = await _gameTypeRepository.GetAllAsync(gameId);
                return gameTypes.ToDtos(); // GameTypeMapper Collection Method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all game types for game with ID: {GameId}", gameId);
                throw;
            }
        }

        public async Task<GameTypeDto> GetByIdAsync(int gameId, int id)
        {
            try
            {
                _logger.LogInformation("Retrieving game type with ID: {Id} for game with ID: {GameId}", id, gameId);
                var gameType = await _gameTypeRepository.GetByIdAsync(gameId, id);
                return GameMapper.ToDto(gameType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving game type with ID: {Id} for game with ID: {GameId}", id, gameId);
                throw;
            }
        }

        public async Task<GameTypeDto> AddAsync(int gameId, GameTypeCreateDto gameTypeDto)
        {
            try
            {
                _logger.LogInformation("Adding game type with ID: {GameTypeId} for game with ID: {GameId}", gameTypeDto.TypeId, gameId);
                var gameType = GameMapper.ToEntity(gameId, gameTypeDto);
                var addedGameType = await _gameTypeRepository.AddAsync(gameType);
                return GameMapper.ToDto(addedGameType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding game type with ID: {GameTypeId} for game with ID: {GameId}", gameTypeDto.TypeId, gameId);
                throw;
            }
        }

        public async Task UpdateAsync(int gameId, int id, GameTypeUpdateDto gameTypeDto)
        {
            try
            {
                var gameType = await _gameTypeRepository.GetByIdAsync(gameId, id);
                gameType.UpdateFromDto(gameId, gameTypeDto);
                _logger.LogInformation("Updating game type with ID: {Id} for game with ID: {GameId}", id, gameId);
                await _gameTypeRepository.UpdateAsync(gameType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating game type with ID: {Id} for game with ID: {GameId}", id, gameId);
                throw;
            }
        }

        public async Task DeleteAsync(int gameId, int id)
        {
            try
            {
                var gameType = await _gameTypeRepository.GetByIdAsync(gameId, id);
                await _gameTypeRepository.DeleteAsync(gameId, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting game type with ID: {Id} for game with ID: {GameId}", id, gameId);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int gameId, int id)
        {
            try
            {
                _logger.LogInformation("Checking existence of game type with ID: {Id} for game with ID: {GameId}", id, gameId);
                return await _gameTypeRepository.ExistsAsync(gameId, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of game type with ID: {Id} for game with ID: {GameId}", id, gameId);
                throw;
            }
        }
    }
}
