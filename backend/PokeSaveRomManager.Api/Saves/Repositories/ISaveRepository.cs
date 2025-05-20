using Microsoft.EntityFrameworkCore.Storage;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Saves.Repositories
{
    public interface ISaveRepository
    {
        Task<(IEnumerable<Save> Saves, int TotalCount)> GetAllAsync(string userId, int pageNumber, int pageSize);
        Task<Save> GetByIdAsync(int saveId);
        Task<Save> Create(Save save);
        Task<Save> Update(Save save);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<PokemonInstance> GetPokemonInstanceByIdAsync(int pokemonInstanceId);
        Task<PokemonInstance> CreatePokemon(PokemonInstance pokemonInstance);
        Task<PokemonInstance> UpdatePokemon(PokemonInstance pokemonInstance);

        Task SaveChangesAsync();
    }
}
