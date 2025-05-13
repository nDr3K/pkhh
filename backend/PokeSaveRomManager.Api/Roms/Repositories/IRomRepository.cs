using Microsoft.EntityFrameworkCore.Storage;

namespace PokeSaveRomManager.Api.Roms.Repositories
{
    public interface IRomRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
