using PokeSaveRomManager.Api.Roms.DTOs;

namespace PokeSaveRomManager.Api.Roms.Services
{
    public interface IRomService
    {
        Task UploadRomAsync(RomUploadDto romDto);
    }
}
