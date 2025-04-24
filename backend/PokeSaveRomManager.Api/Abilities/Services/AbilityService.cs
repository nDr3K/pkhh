using PokeSaveRomManager.Api.Abilities.DTOs;
using PokeSaveRomManager.Api.Abilities.Mapper;
using PokeSaveRomManager.Api.Abilities.Repositories;

namespace PokeSaveRomManager.Api.Abilities.Services
{
    public class AbilityService : IAbilityService
    {
        private readonly IAbilityRepository _abilityRepository;
        private readonly ILogger<AbilityService> _logger;

        public AbilityService(IAbilityRepository abilityRepository, ILogger<AbilityService> logger)
        {
            _abilityRepository = abilityRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<AbilityDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all abilities");
                var abilities = await _abilityRepository.GetAllAsync();
                return abilities.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching abilities");
                throw;
            }
        }

        public async Task<AbilityDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching ability with ID: {id}");
                var ability = await _abilityRepository.GetByIdAsync(id);
                return ability.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching ability with ID: {id}");
                throw;
            }
        }

        public async Task<AbilityDto> CreateAsync(AbilityCreateDto abilityCreateDto)
        {
            try
            {
                _logger.LogInformation("Creating new ability");
                var ability = abilityCreateDto.ToEntity();
                var createdAbility = await _abilityRepository.CreateAsync(ability);
                return createdAbility.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ability");
                throw;
            }
        }

        public async Task UpdateAsync(int id, AbilityUpdateDto abilityUpdateDto)
        {
            try
            {
                _logger.LogInformation($"Updating ability with ID: {id}");
                var ability = await _abilityRepository.GetByIdAsync(id);
                ability.UpdateFromDto(abilityUpdateDto);
                await _abilityRepository.UpdateAsync(ability);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating ability with ID: {id}");
                throw;
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting ability with ID: {id}");
                await _abilityRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting ability with ID: {id}");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Checking existence of ability with ID: {id}");
                return await _abilityRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking existence of ability with ID: {id}");
                throw;
            }
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            try
            {
                _logger.LogInformation($"Checking existence of ability with name: {name}");
                return await _abilityRepository.ExistsByNameAsync(name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking existence of ability with name: {name}");
                throw;
            }
        }
    }
}
