using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Games.Mapper;
using PokeSaveRomManager.Api.Games.Repositories;

namespace PokeSaveRomManager.Api.Games.Services
{
    public class GameAbilityService : IGameAbilityService
    {
        private readonly IGameAbilityRepository _gameAbilityRepository;
        private readonly ILogger<GameAbilityService> _logger;

        public GameAbilityService(IGameAbilityRepository gameAbilityRepository, ILogger<GameAbilityService> logger)
        {
            _gameAbilityRepository = gameAbilityRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<GameAbilityDto>> GetAllAsync(int gameId)
        {
            try
            {
                _logger.LogInformation("Retrieving all abilities for game with ID: {gameId}", gameId);
                var gameAbilities = await _gameAbilityRepository.GetAllAsync(gameId);
                return gameAbilities.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all abilities for game with ID: {gameId}", gameId);
                throw;
            }
        }

        public async Task<GameAbilityDto> GetByIdAsync(int gameId, int id)
        {
            try
            {
                _logger.LogInformation("Retrieving ability with ID: {Id} for game with ID: {gameId}", id, gameId);
                var gameAbility = await _gameAbilityRepository.GetByIdAsync(gameId, id);
                return GameMapper.ToDto(gameAbility);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving ability with ID: {Id} for game with ID: {gameId}", id, gameId);
                throw;
            }
        }

        public async Task<GameAbilityDto> AddAsync(int gameId, GameAbilityCreateDto gameAbilityDto)
        {
            try
            {
                _logger.LogInformation("Adding new ability with ID: {abilityId} for game with ID: {gameId}", gameAbilityDto.AbilityId, gameId);
                var gameAbility = GameMapper.ToEntity(gameId, gameAbilityDto);
                var addedGameAbility = await _gameAbilityRepository.AddAsync( gameAbility);
                return GameMapper.ToDto(addedGameAbility);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding ability with ID: {abilityId} for game with ID: {gameId}", gameAbilityDto.AbilityId, gameId);
                throw;
            }
        }

        public async Task UpdateAsync(int gameId, int id, GameAbilityUpdateDto gameAbilityDto)
        {
            try
            {
                _logger.LogInformation("Updating ability with ID: {Id} for game with ID: {gameId}", id, gameId);
                var gameAbility = await _gameAbilityRepository.GetByIdAsync(gameId, id);
                gameAbility.UpdateFromDto(gameId, gameAbilityDto);
                await _gameAbilityRepository.UpdateAsync(gameAbility);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating ability with ID: {Id} for game with ID: {gameId}", id, gameId);
                throw;
            }
        }

        public async Task DeleteAsync(int gameId, int id)
        {
            try
            {
                _logger.LogInformation("Deleting ability with ID: {Id} for game with ID: {gameId}", id, gameId);
                await _gameAbilityRepository.DeleteAsync(gameId, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting ability with ID: {Id} for game with ID: {gameId}", id, gameId);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int gameId, int id)
        {
            try
            {
                _logger.LogInformation("Checking existence of ability with ID: {Id} for game with ID: {gameId}", id, gameId);
                return await _gameAbilityRepository.ExistsAsync(gameId, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of game ability with ID: {Id} for game with ID: {gameId}", id, gameId);
                throw;
            }
        }
    }
}
