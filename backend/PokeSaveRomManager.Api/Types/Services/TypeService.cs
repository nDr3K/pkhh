using PokeSaveRomManager.Api.Types.DTOs;
using PokeSaveRomManager.Api.Types.Mapper;
using PokeSaveRomManager.Api.Types.Repositories;

namespace PokeSaveRomManager.Api.Types.Services
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _typeRepository;
        private readonly ILogger<TypeService> _logger;

        public TypeService(ITypeRepository typeRepository, ILogger<TypeService> logger)
        {
            _typeRepository = typeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TypeDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all types");
                var types = await _typeRepository.GetAllAsync();
                return types.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all types");
                throw;
            }
        }

        public async Task<TypeDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving type with ID: {Id}", id);
                var type = await _typeRepository.GetByIdAsync(id);
                return TypeMapper.ToDto(type);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving type with ID: {Id}", id);
                throw;
            }
        }

        public async Task<TypeDto> AddAsync(TypeCreateDto typeDto)
        {
            try
            {
                _logger.LogInformation("Adding new type: {TypeName}", typeDto.Name);
                var type = TypeMapper.ToEntity(typeDto);
                var addedType = await _typeRepository.AddAsync(type);
                return TypeMapper.ToDto(addedType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding type: {TypeName}", typeDto.Name);
                throw;
            }
        }

        public async Task UpdateAsync(int id, TypeUpdateDto typeDto)
        {
            try
            {
                var type = await _typeRepository.GetByIdAsync(id);
                type.UpdateFromDto(typeDto);
                _logger.LogInformation("Updating type with ID: {Id}", id);
                await _typeRepository.UpdateAsync(type);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating type with ID: {Id}", id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting type with ID: {Id}", id);
                await _typeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting type with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                _logger.LogInformation("Checking if type exists with ID: {Id}", id);
                return await _typeRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if type exists with ID: {Id}", id);
                throw;
            }
        }
    }
}
