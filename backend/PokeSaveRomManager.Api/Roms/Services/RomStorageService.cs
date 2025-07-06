
using PokeSaveRomManager.Api.Roms.DTOs;

namespace PokeSaveRomManager.Api.Roms.Services
{
    public class RomStorageService : IRomStorageService
    {
        private readonly string DATA_DIRECTORY = "data";
        private readonly string ROMS_DIRECTORY = "roms";
        public async Task<string> PersistRom(RomUploadDto romDto)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), DATA_DIRECTORY, ROMS_DIRECTORY);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{romDto.RomFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await romDto.RomFile.CopyToAsync(fileStream);
            }

            return $"{DATA_DIRECTORY}/{ROMS_DIRECTORY}/{uniqueFileName}";
        }
    }
}
