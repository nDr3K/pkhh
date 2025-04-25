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

        //Ability
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

        //AbilityName
        public async Task<IEnumerable<AbilityNameDto>> GetAllNamesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all ability names");
                var abilityNames = await _abilityRepository.GetAllNamesAsync();
                return abilityNames.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching ability names");
                throw;
            }
        }

        public async Task<AbilityNameDto> GetNameByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching ability name with ID: {id}");
                var abilityName = await _abilityRepository.GetNameByIdAsync(id);
                return abilityName.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching ability name with ID: {id}");
                throw;
            }
        }

        public async Task<AbilityNameDto> CreateNameAsync(AbilityNameCreateDto abilityNameCreateDto)
        {
            try
            {
                _logger.LogInformation("Creating new ability name");
                var abilityName = abilityNameCreateDto.ToEntity();
                var createdAbilityName = await _abilityRepository.CreateNameAsync(abilityName);
                return createdAbilityName.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ability name");
                throw;
            }
        }

        public async Task UpdateNameAsync(int id, AbilityNameUpdateDto abilityNameUpdateDto)
        {
            try
            {
                _logger.LogInformation($"Updating ability name with ID: {id}");
                var abilityName = await _abilityRepository.GetNameByIdAsync(id);
                abilityName.UpdateFromDto(abilityNameUpdateDto);
                await _abilityRepository.UpdateNameAsync(abilityName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating ability name with ID: {id}");
                throw;
            }
        }

        public async Task DeleteNameAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting ability name with ID: {id}");
                await _abilityRepository.DeleteNameAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting ability name with ID: {id}");
                throw;
            }
        }

        public async Task<bool> ExistsNameAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Checking existence of ability name with ID: {id}");
                return await _abilityRepository.ExistsNameAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking existence of ability name with ID: {id}");
                throw;
            }
        }
    }
}
