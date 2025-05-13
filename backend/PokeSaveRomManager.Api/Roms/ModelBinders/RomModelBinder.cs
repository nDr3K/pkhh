using Microsoft.AspNetCore.Mvc.ModelBinding;
using PokeSaveRomManager.Api.Roms.DTOs;
using System.Text.Json;

namespace PokeSaveRomManager.Api.Roms.ModelBinders
{
    public class RomModelBinder : IModelBinder
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
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,"Metadata is required.");
                    return Task.CompletedTask;
                }

                // Get the ROM file
                var romFile = form.Files.GetFile("RomFile");
                if (romFile == null)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,"ROM file is required.");
                    return Task.CompletedTask;
                }

                // Deserialize metadata
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var metadata = JsonSerializer.Deserialize<RomCreateDto>(metadataJson, options);

                // Create the upload DTO
                var romUploadDto = new RomUploadDto
                {
                    RomFile = romFile,
                    Metadata = metadata
                };

                // Set the successfully bound model
                bindingContext.Result = ModelBindingResult.Success(romUploadDto);
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,$"Error processing upload: {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
