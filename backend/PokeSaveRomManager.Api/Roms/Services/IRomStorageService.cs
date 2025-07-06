using PokeSaveRomManager.Api.Roms.DTOs;

namespace PokeSaveRomManager.Api.Roms.Services
{
    public interface IRomStorageService
    {
        public Task<string> PersistRom(RomUploadDto romDto);
    }
}
