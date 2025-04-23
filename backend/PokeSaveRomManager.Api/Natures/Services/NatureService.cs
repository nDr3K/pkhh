using PokeSaveRomManager.Api.Natures.DTOs;
using PokeSaveRomManager.Api.Natures.Mapper;
using PokeSaveRomManager.Api.Natures.Repositories;

namespace PokeSaveRomManager.Api.Natures.Services
{
    public class NatureService : INatureService
    {
        private readonly INatureRepository _natureRepository;
        private readonly ILogger<NatureService> _logger;

        public NatureService(INatureRepository natureRepository, ILogger<NatureService> logger)
        {
            _natureRepository = natureRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<NatureDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all natures");
                var natures = await _natureRepository.GetAllAsync();
                return natures.ToDtos(); // NatureMapper Collection Method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all natures");
                throw;
            }
        }

        public async Task<NatureDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving nature with ID: {Id}", id);
                var nature = await _natureRepository.GetByIdAsync(id);
                return NatureMapper.ToDto(nature);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving nature with ID: {Id}", id);
                throw;
            }
        }

        public async Task<NatureDto> AddAsync(NatureCreateDto natureDto)
        {
            try
            {
                _logger.LogInformation("Adding new nature: {NatureName}", natureDto.Name);
                var nature = NatureMapper.ToEntity(natureDto);
                var addedNature = await _natureRepository.AddAsync(nature);
                return NatureMapper.ToDto(addedNature);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding nature: {NatureName}", natureDto.Name);
                throw;
            }
        }

        public async Task UpdateAsync(int id, NatureUpdateDto natureDto)
        {
            try
            {
                var nature = await _natureRepository.GetByIdAsync(id);
                if (nature == null)
                {
                    _logger.LogWarning("Nature with ID: {Id} not found", id);
                    throw new KeyNotFoundException($"Nature with ID: {id} not found");
                }
                nature.UpdateFromDto(natureDto);
                await _natureRepository.UpdateAsync(nature);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating nature with ID: {Id}", id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting nature with ID: {Id}", id);
                await _natureRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting nature with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                _logger.LogInformation("Checking existence of nature with ID: {Id}", id);
                return await _natureRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of nature with ID: {Id}", id);
                throw;
            }
        }
    }
}
