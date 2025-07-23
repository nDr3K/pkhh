using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using PokeSaveRomManager.Api.Saves.DTOs;
using System.Text.Json;

namespace PokeSaveRomManager.Api.Saves.ModelBinders
{
    public class SaveFileModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            try
            {
                var form = bindingContext.HttpContext.Request.Form;

                // Deserialize Metadata
                var metadataJson = form["Metadata"];

                // Get the ROM file
                var saveFile = form.Files.GetFile("SaveFile");

                // Deserialize metadata
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                SaveFileDto? metadata = null;

                if (!StringValues.IsNullOrEmpty(metadataJson))
                {
                    metadata = JsonSerializer.Deserialize<SaveFileDto>(metadataJson, options);
                }

                // Create the upload DTO
                var saveFilUpload = new SaveFileUploadDto
                {
                    SaveFile = saveFile,
                    Metadata = metadata
                };

                // Set the successfully bound model
                bindingContext.Result = ModelBindingResult.Success(saveFilUpload);
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"Error processing upload: {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
