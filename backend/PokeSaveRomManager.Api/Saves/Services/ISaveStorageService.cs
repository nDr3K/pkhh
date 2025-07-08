using PokeSaveRomManager.Api.Saves.DTOs;

namespace PokeSaveRomManager.Api.Saves.Services
{
    public interface ISaveStorageService
    {
        public Task<string> PersistSave(SaveFileUploadDto saveDto);
        public bool DeleteSave(string saveFilePath);
    }
}
