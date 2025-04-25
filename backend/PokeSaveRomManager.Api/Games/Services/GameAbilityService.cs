using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Games.Mapper;
using PokeSaveRomManager.Api.Games.Repositories;

namespace PokeSaveRomManager.Api.Games.Services
{
    public class GameAbilityService : IGameAbilityService
    {
        private readonly IGameAbilityRepository _gameAbilityRepository;
        private readonly ILogger<GameAbilityService> _logger;

        public GameAbilityService(IGameAbilityRepository gameAbilityRepository)
        {
            _gameAbilityRepository = gameAbilityRepository;
        }

        public async Task<IEnumerable<GameAbilityDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all game abilities");
                var gameAbilities = await _gameAbilityRepository.GetAllAsync();
                return gameAbilities.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all game abilities");
                throw;
            }
        }

        public async Task<GameAbilityDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving game ability with ID: {Id}", id);
                var gameAbility = await _gameAbilityRepository.GetByIdAsync(id);
                return GameMapper.ToDto(gameAbility);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving game ability with ID: {Id}", id);
                throw;
            }
        }

        public async Task<GameAbilityDto> AddAsync(GameAbilityCreateDto gameAbilityDto)
        {
            try
            {
                _logger.LogInformation("Adding new game ability {GameAbilityAbilityId} for {GameAbilityGameId}", gameAbilityDto.AbilityId, gameAbilityDto.GameId);
                var gameAbility = GameMapper.ToEntity(gameAbilityDto);
                var addedGameAbility = await _gameAbilityRepository.AddAsync(gameAbility);
                return GameMapper.ToDto(addedGameAbility);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding game ability {GameAbilityAbilityId} for {GameAbilityGameId}", gameAbilityDto.AbilityId, gameAbilityDto.GameId);
                throw;
            }
        }

        public async Task UpdateAsync(int id, GameAbilityUpdateDto gameAbilityDto)
        {
            try
            {
                _logger.LogInformation("Updating game ability with ID: {Id}", id);
                var gameAbility = await _gameAbilityRepository.GetByIdAsync(id);
                gameAbility.UpdateFromDto(gameAbilityDto);
                await _gameAbilityRepository.UpdateAsync(gameAbility);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating game ability with ID: {Id}", id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting game ability with ID: {Id}", id);
                await _gameAbilityRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting game ability with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                _logger.LogInformation("Checking existence of game ability with ID: {Id}", id);
                return await _gameAbilityRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of game ability with ID: {Id}", id);
                throw;
            }
        }
    }
}
