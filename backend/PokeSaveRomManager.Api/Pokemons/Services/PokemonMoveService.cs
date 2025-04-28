using PokeSaveRomManager.Api.Pokemons.DTOs;
using PokeSaveRomManager.Api.Pokemons.Mapper;
using PokeSaveRomManager.Api.Pokemons.Repositories;

namespace PokeSaveRomManager.Api.Pokemons.Services
{
    public class PokemonMoveService : IPokemonMoveService
    {
        private readonly IPokemonMoveRepository _repository;
        private readonly ILogger<PokemonMoveService> _logger;

        public PokemonMoveService(IPokemonMoveRepository repository, ILogger<PokemonMoveService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<PokemonMoveDto>> GetAllAsync(int pokemonId)
        {
            try
            {
                var moves = await _repository.GetAllAsync(pokemonId);
                _logger.LogInformation("Retrieved {Count} moves for Pokemon with ID {PokemonId}", moves.Count(), pokemonId);
                return moves.ToDtos();
            }
            catch
            {
                _logger.LogError("Error retrieving moves for Pokemon with ID {PokemonId}", pokemonId);
                throw;
            }
        }

        public async Task<PokemonMoveDto> GetByIdAsync(int id)
        {
            try
            {
                var move = await _repository.GetByIdAsync(id);
                _logger.LogInformation("Retrieved move with ID {Id}", id);
                return move.ToDto();
            }
            catch
            {
                _logger.LogError("Error retrieving move with ID {Id}", id);
                throw;
            }
        }

        public async Task<PokemonMoveDto> CreateAsync(int pokemonId, PokemonMoveCreateDto dto)
        {
            try
            {
                var move = dto.ToEntity(pokemonId);
                var createdMove = await _repository.CreateAsync(move);
                _logger.LogInformation("Created move with ID {Id} for Pokemon with ID {PokemonId}", createdMove.Id, pokemonId);
                return createdMove.ToDto();
            }
            catch
            {
                _logger.LogError("Error creating move for Pokemon with ID {PokemonId}", pokemonId);
                throw;
            }
        }

        public async Task UpdateAsync(int id, int pokemonId, PokemonMoveUpdateDto dto)
        {
            try
            {
                var move = await _repository.GetByIdAsync(id);
                move.UpdateFromDto(pokemonId, dto);
                await _repository.UpdateAsync(move);
                _logger.LogInformation("Updated move with ID {Id}", id);
            }
            catch
            {
                _logger.LogError("Error updating move with ID {Id}", id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                _logger.LogInformation("Deleted move with ID {Id}", id);
            }
            catch
            {
                _logger.LogError("Error deleting move with ID {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistASync(int id)
        {
            try
            {
                var exists = await _repository.ExistAsync(id);
                _logger.LogInformation("Move with ID {Id} exists: {Exists}", id, exists);
                return exists;
            }
            catch
            {
                _logger.LogError("Error checking existence of move with ID {Id}", id);
                throw;
            }
        }
    }
}
