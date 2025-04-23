using PokeSaveRomManager.Api.Stats.DTOs;
using PokeSaveRomManager.Api.Stats.Mapper;
using PokeSaveRomManager.Api.Stats.Repositories;

namespace PokeSaveRomManager.Api.Stats.Services
{
    public class StatService : IStatService
    {
        private readonly IStatRepository _statRepository;
        private readonly ILogger<StatService> _logger;

        public StatService(IStatRepository statRepository, ILogger<StatService> logger)
        {
            _statRepository = statRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<StatDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all stats");
                var stats = await _statRepository.GetAllAsync();
                return stats.ToDtos(); // StatMapper Collection Method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all stats");
                throw;
            }
        }

        public async Task<StatDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving stat with ID: {Id}", id);
                var stat = await _statRepository.GetByIdAsync(id);
                return StatMapper.ToDto(stat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving stat with ID: {Id}", id);
                throw;
            }
        }

        public async Task<StatDto> AddAsync(StatCreateDto statDto)
        {
            try
            {
                _logger.LogInformation("Adding new stat: {StatName}", statDto.Name);
                var stat = StatMapper.ToEntity(statDto);
                var addedStat = await _statRepository.AddAsync(stat);
                return StatMapper.ToDto(addedStat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding stat: {StatName}", statDto.Name);
                throw;
            }
        }

        public async Task UpdateAsync(int id, StatUpdateDto statDto)
        {
            try
            {
                var stat = await _statRepository.GetByIdAsync(id);
                if (stat == null)
                {
                    _logger.LogWarning("Stat with ID: {Id} not found", id);
                    throw new KeyNotFoundException($"Stat with ID: {id} not found");
                }
                stat.UpdateFromDto(statDto);
                await _statRepository.UpdateAsync(stat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating stat with ID: {Id}", id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting stat with ID: {Id}", id);
                await _statRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting stat with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                return await _statRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if stat exists with ID: {Id}", id);
                throw;
            }
        }
    }
}
