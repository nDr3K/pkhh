using PokeSaveRomManager.Api.Pokemons.DTOs;
using PokeSaveRomManager.Api.Pokemons.Mapper;
using PokeSaveRomManager.Api.Pokemons.Repositories;

namespace PokeSaveRomManager.Api.Pokemons.Services
{
    public class PokemonFormService : IPokemonFormService
    {
        private readonly IPokemonFormRepository _repository;
        private readonly ILogger<PokemonFormService> _logger;

        public PokemonFormService(IPokemonFormRepository repository, ILogger<PokemonFormService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<PokemonFormDto> GetByIdAsync(int id)
        {
            try
            {
                var form = await _repository.GetByIdAsync(id);
                _logger.LogInformation($"Retrieved PokemonForm with ID {id}");
                return form.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving PokemonForm with ID {id}");
                throw;
            }
        }

        public async Task<IEnumerable<PokemonFormDto>> GetAllAsync(int pokemonId)
        {
            try
            {
                var forms = await _repository.GetAllAsync(pokemonId);
                _logger.LogInformation($"Retrieved {forms.Count()} PokemonForms for Pokemon ID {pokemonId}");
                return forms.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving PokemonForms for Pokemon ID {pokemonId}");
                throw;
            }
        }

        public async Task<PokemonFormDto> CreateAsync(int pokemonId, PokemonFormCreateDto form)
        {
            try
            {
                var entity = form.ToEntity(pokemonId);
                var createdForm = await _repository.CreateAsync(entity);
                _logger.LogInformation($"Created PokemonForm with ID {createdForm.Id} for Pokemon ID {pokemonId}");
                return createdForm.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating PokemonForm for Pokemon ID {pokemonId}");
                throw;
            }
        }

        public async Task<IEnumerable<PokemonFormDto>> AddRangeAsync(IEnumerable<PokemonFormCreateDto> forms)
        {
            try
            {
                var entities = forms.ToEntities();
                var createdForms = await _repository.AddRangeAsync(entities);
                _logger.LogInformation($"Created {createdForms.Count()} PokemonForms");
                return createdForms.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating multiple PokemonForms");
                throw;
            }
        }

        public async Task UpdateAsync(int id, int pokemonId, PokemonFormUpdateDto form)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                entity.UpdateFromDto(pokemonId, form);
                await _repository.UpdateAsync(entity);
                _logger.LogInformation($"Updated PokemonForm with ID {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating PokemonForm with ID {id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                _logger.LogInformation($"Deleted PokemonForm with ID {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting PokemonForm with ID {id}");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                var exists = await _repository.ExistsAsync(id);
                _logger.LogInformation($"Checked existence of PokemonForm with ID {id}: {exists}");
                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking existence of PokemonForm with ID {id}");
                throw;
            }
        }
    }
}
