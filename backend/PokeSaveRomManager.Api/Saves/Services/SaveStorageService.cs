using PokeSaveRomManager.Api.Saves.DTOs;

namespace PokeSaveRomManager.Api.Saves.Services
{
    public class SaveStorageService : ISaveStorageService
    {
        private readonly string DATA_DIRECTORY = "data";
        private readonly string SAVES_DIRECTORY = "saves";
        public async Task<string> PersistSave(SaveFileUploadDto saveDto)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), DATA_DIRECTORY, SAVES_DIRECTORY);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{saveDto.SaveFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await saveDto.SaveFile.CopyToAsync(fileStream);
            }

            return $"{DATA_DIRECTORY}/{SAVES_DIRECTORY}/{uniqueFileName}";
        }

        public bool DeleteSave(string saveFilePath)
        {
            if (File.Exists(saveFilePath))
            {
                try
                {
                    File.Delete(saveFilePath);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
