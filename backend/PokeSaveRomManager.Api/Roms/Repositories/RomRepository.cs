using Microsoft.EntityFrameworkCore.Storage;
using PokeSaveRomManager.Data;

namespace PokeSaveRomManager.Api.Roms.Repositories
{
    public class RomRepository : IRomRepository
    {
        private readonly PokemonDbContext _context;

        public RomRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
