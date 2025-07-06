using PokeSaveRomManager.Api.Roms.DTOs;
using PokeSaveRomManager.Parser.Core.Models;

namespace PokeSaveRomManager.Api.Roms.Services.Handler
{
    public interface IRomServiceHandler
    {
        Task RegisterRomDataAsync(RomSaveDto dto, ParsedRomData data);
    }
}
