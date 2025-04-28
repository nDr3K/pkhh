using PokeSaveRomManager.Api.Saves.DTOs;
using PokeSaveRomManager.Api.Saves.Mapper;
using PokeSaveRomManager.Api.Saves.Repositories;

namespace PokeSaveRomManager.Api.Saves.Services
{
    public class SaveService : ISaveService
    {
        private readonly ISaveRepository _repository;
        private readonly ILogger<SaveService> _logger;

        public SaveService(ISaveRepository repository, ILogger<SaveService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<(IEnumerable<SaveDto> Saves, int TotalCount)> GetAllAsync(string gameId, int pageNumber, int pageSize)
        {
            try
            {
                var (saves, totalCount) = await _repository.GetAllAsync(gameId, pageNumber, pageSize);
                _logger.LogInformation("Retrieved {Count} saves for Game with ID {GameId}", saves.Count(), gameId);
                return (saves.ToDtos(), totalCount);
            }
            catch
            {
                _logger.LogError("Error retrieving saves for Game with ID {GameId}", gameId);
                throw;
            }
        }

        public async Task<SaveDetailDto> GetByIdAsync(int id)
        {
            try
            {
                var save = await _repository.GetByIdAsync(id);
                _logger.LogInformation("Retrieved save with ID {Id}", id);
                return save.ToDetailDto();
            }
            catch
            {
                _logger.LogError("Error retrieving save with ID {Id}", id);
                throw;
            }
        }
    }
}
