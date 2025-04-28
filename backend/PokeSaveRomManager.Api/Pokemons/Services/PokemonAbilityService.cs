using PokeSaveRomManager.Api.Pokemons.DTOs;
using PokeSaveRomManager.Api.Pokemons.Mapper;
using PokeSaveRomManager.Api.Pokemons.Repositories;

namespace PokeSaveRomManager.Api.Pokemons.Services
{
    public class PokemonAbilityService : IPokemonAbilityService
    {
        private readonly IPokemonAbilityRepository _repository;
        private readonly ILogger<PokemonAbilityService> _logger;

        public PokemonAbilityService(IPokemonAbilityRepository repository, ILogger<PokemonAbilityService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<PokemonAbilityDto>> GetAllAsync(int pokemonId)
        {
            try
            {
                var abilities = await _repository.GetAllAsync(pokemonId);
                _logger.LogInformation("Fetched {Count} abilities for Pokemon with ID {PokemonId}", abilities.Count(), pokemonId);
                return abilities.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all abilities for Pokemon with ID {PokemonId}", pokemonId);
                throw;
            }
        }

        public async Task<PokemonAbilityDto> GetByIdAsync(int id)
        {
            try
            {
                var ability = await _repository.GetByIdAsync(id);
                _logger.LogInformation("Fetched ability with ID {Id}", id);
                return ability.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching ability with ID {Id}", id);
                throw;
            }
        }

        public async Task<PokemonAbilityDto> CreateAsync(int pokemonId, PokemonAbilityCreateDto dto)
        {
            try
            {
                var ability = dto.ToEntity(pokemonId);
                var createdAbility = await _repository.CreateAsync(ability);
                _logger.LogInformation("Created new ability with ID {Id} for Pokemon with ID {PokemonId}", createdAbility.Id, pokemonId);
                return createdAbility.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ability for Pokemon with ID {PokemonId}", pokemonId);
                throw;
            }
        }

        public async Task UpdateAsync(int id, int pokemonId, PokemonAbilityUpdateDto dto)
        {
            try
            {
                var ability = await _repository.GetByIdAsync(id);
                ability.UpdateFromDto(pokemonId, dto);
                await _repository.UpdateAsync(ability);
                _logger.LogInformation("Updated ability with ID {Id} for Pokemon with ID {PokemonId}", id, pokemonId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ability with ID {Id}", id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                _logger.LogInformation("Deleted ability with ID {Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting ability with ID {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                var exists = await _repository.ExistsAsync(id);
                _logger.LogInformation("Ability with ID {Id} exists: {Exists}", id, exists);
                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking existence of ability with ID {Id}", id);
                throw;
            }
        }
    }
}
