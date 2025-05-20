using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Saves.Repositories
{
    public interface IBoxRepository
    {
        Task<SaveBox> Create(SaveBox saveBox);
        Task Delete(int boxId);
    }
}
