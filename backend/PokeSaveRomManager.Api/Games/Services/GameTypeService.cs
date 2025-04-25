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

        public async Task<IEnumerable<GameTypeDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all game types");
                var gameTypes = await _gameTypeRepository.GetAllAsync();
                return gameTypes.ToDtos(); // GameTypeMapper Collection Method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all game types");
                throw;
            }
        }

        public async Task<GameTypeDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving game type with ID: {Id}", id);
                var gameType = await _gameTypeRepository.GetByIdAsync(id);
                return GameMapper.ToDto(gameType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving game type with ID: {Id}", id);
                throw;
            }
        }

        public async Task<GameTypeDto> AddAsync(GameTypeCreateDto gameTypeDto)
        {
            try
            {
                _logger.LogInformation("Adding new game type {GameTypeTypeId} for {GameTypeGameId}", gameTypeDto.TypeId, gameTypeDto.GameId);
                var gameType = GameMapper.ToEntity(gameTypeDto);
                var addedGameType = await _gameTypeRepository.AddAsync(gameType);
                return GameMapper.ToDto(addedGameType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding game type{GameTypeTypeId} for {GameTypeGameId}", gameTypeDto.TypeId, gameTypeDto.GameId);
                throw;
            }
        }

        public async Task UpdateAsync(int id, GameTypeUpdateDto gameTypeDto)
        {
            try
            {
                var gameType = await _gameTypeRepository.GetByIdAsync(id);
                gameType.UpdateFromDto(gameTypeDto);
                _logger.LogInformation("Updating game type with ID: {Id}", gameType.Id);
                await _gameTypeRepository.UpdateAsync(gameType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating game type with ID: {Id}", id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting game type with ID: {Id}", id);
                await _gameTypeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting game type with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                _logger.LogInformation("Checking if game type with ID: {Id} exists", id);
                return await _gameTypeRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of game type with ID: {Id}", id);
                throw;
            }
        }
    }
}
