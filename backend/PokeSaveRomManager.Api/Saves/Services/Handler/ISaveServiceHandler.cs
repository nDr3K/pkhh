using PokeSaveRomManager.Api.Saves.Models;
using PokeSaveRomManager.Data.Domain;
using PokeSaveRomManager.Parser.Core.Models;

namespace PokeSaveRomManager.Api.Saves.Services.Handler
{
    public interface ISaveServiceHandler
    {
        Task<SaveFileData> GetDatas(ParsedSaveData saveData, int gameId);
        Task DeletePokemonInstances(int saveId);
        Task DeleteParty(int saveId);
        Task DeleteBox(int boxId);
        Task<SaveBox> CreateBox(SaveBox saveBox);
        Task<IEnumerable<PokemonInstance>> SavePokemonInstances(IEnumerable<PokemonData> pokemonData, int saveId);
    }
}
