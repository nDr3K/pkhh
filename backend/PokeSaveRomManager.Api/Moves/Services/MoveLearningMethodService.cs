using PokeSaveRomManager.Api.Moves.DTOs;
using PokeSaveRomManager.Api.Moves.Mapper;
using PokeSaveRomManager.Api.Moves.Repositories;

namespace PokeSaveRomManager.Api.Moves.Services
{
    public class MoveLearningMethodService : IMoveLearningMethodService
    {
        private readonly IMoveLearningMethodRepository _moveLearningMethodRepository;
        private readonly ILogger<MoveLearningMethodService> _logger;

        public MoveLearningMethodService(IMoveLearningMethodRepository moveLearningMethodRepository, ILogger<MoveLearningMethodService> logger)
        {
            _moveLearningMethodRepository = moveLearningMethodRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MoveLearningMethodDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all move learning methods");
                var moveLearningMethods = await _moveLearningMethodRepository.GetAllAsync();
                return moveLearningMethods.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all move learning methods");
                throw;
            }
        }

        public async Task<MoveLearningMethodDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving move learning method with ID {id}");
                var moveLearningMethod = await _moveLearningMethodRepository.GetByIdAsync(id);
                return moveLearningMethod.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving move learning method with ID {id}");
                throw;
            }
        }

        public async Task<MoveLearningMethodDto> CreateAsync(MoveLearningMethodCreateDto moveLearningMethodDto)
        {
            try
            {
                _logger.LogInformation("Creating new move learning method");
                var moveLearningMethod = moveLearningMethodDto.ToEntity();
                var createdMoveLearningMethod = await _moveLearningMethodRepository.CreateAsync(moveLearningMethod);
                return createdMoveLearningMethod.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new move learning method");
                throw;
            }
        }

        public async Task UpdateAsync(int id, MoveLearningMethodUpdateDto moveLearningMethodDto)
        {
            try
            {
                _logger.LogInformation($"Updating move learning method with ID {id}");
                var moveLearningMethod = await _moveLearningMethodRepository.GetByIdAsync(id);
                moveLearningMethod.UpdateFromDto(moveLearningMethodDto);
                await _moveLearningMethodRepository.UpdateAsync(moveLearningMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating move learning method with ID {id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting move learning method with ID {id}");
                await _moveLearningMethodRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting move learning method with ID {id}");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Checking if move learning method with ID {id} exists");
                return await _moveLearningMethodRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking if move learning method with ID {id} exists");
                throw;
            }
        }
    }
}
