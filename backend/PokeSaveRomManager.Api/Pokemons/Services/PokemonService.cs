using PokeSaveRomManager.Api.Pokemons.DTOs;
using PokeSaveRomManager.Api.Pokemons.Mapper;
using PokeSaveRomManager.Api.Pokemons.Repositories;

namespace PokeSaveRomManager.Api.Pokemons.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ILogger<PokemonService> _logger;
        public PokemonService(IPokemonRepository pokemonRepository, ILogger<PokemonService> logger)
        {
            _pokemonRepository = pokemonRepository;
            _logger = logger;
        }

        public async Task<PokemonDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Getting Pokemon by id {Id}", id);
                var pokemon = await _pokemonRepository.GetByIdAsync(id);
                return PokemonMapper.ToDto(pokemon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Pokemon by id {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<PokemonDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Getting all Pokemons");
                var pokemons = await _pokemonRepository.GetAllAsync();
                return PokemonMapper.ToDtos(pokemons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all Pokemons");
                throw;
            }
        }

        public async Task<PokemonDto> CreateAsync(PokemonCreateDto pokemon)
        {
            try
            {
                _logger.LogInformation("Creating Pokemon");
                var entity = PokemonMapper.ToEntity(pokemon);
                var createdPokemon = await _pokemonRepository.CreateAsync(entity);
                return PokemonMapper.ToDto(createdPokemon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Pokemon");
                throw;
            }
        }

        public async Task<IEnumerable<PokemonDto>> AddRangeAsync(IEnumerable<PokemonCreateDto> pokemons)
        {
            try
            {
                _logger.LogInformation("Adding range of Pokemons");
                var entities = pokemons.ToEntities();
                var createdPokemons = await _pokemonRepository.AddRangeAsync(entities);
                return PokemonMapper.ToDtos(createdPokemons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding range of Pokemons");
                throw;
            }
        }

        public async Task UpdateAsync(int id, PokemonUpdateDto pokemon)
        {
            try
            {
                _logger.LogInformation("Updating Pokemon with id {Id}", id);
                var entity = await _pokemonRepository.GetByIdAsync(id);
                entity.UpdateFromDto(pokemon);
                await _pokemonRepository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Pokemon with id {Id}", id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting Pokemon with id {Id}", id);
                await _pokemonRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Pokemon with id {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistAsync(int id)
        {
            try
            {
                _logger.LogInformation("Checking if Pokemon with id {Id} exists", id);
                var pokemon = await _pokemonRepository.GetByIdAsync(id);
                return pokemon != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if Pokemon with id {Id} exists", id);
                throw;
            }
        }

        public async Task<IEnumerable<PokemonDto>> GetByGameIdAsync(int gameId)
        {
            try
            {
                _logger.LogInformation("Getting Pokemons by game id {GameId}", gameId);
                var pokemons = await _pokemonRepository.GetByGameIdAsync(gameId);
                return PokemonMapper.ToDtos(pokemons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Pokemons by game id {GameId}", gameId);
                throw;
            }
        }
    }
}
