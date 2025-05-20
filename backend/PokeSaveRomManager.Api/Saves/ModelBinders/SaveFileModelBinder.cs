using Microsoft.AspNetCore.Mvc.ModelBinding;
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

                if (string.IsNullOrEmpty(metadataJson))
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Metadata is required.");
                    return Task.CompletedTask;
                }

                // Get the ROM file
                var saveFile = form.Files.GetFile("SaveFile");
                if (saveFile == null)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Save file is required.");
                    return Task.CompletedTask;
                }

                // Deserialize metadata
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var metadata = JsonSerializer.Deserialize<SaveFileDto>(metadataJson, options);

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
