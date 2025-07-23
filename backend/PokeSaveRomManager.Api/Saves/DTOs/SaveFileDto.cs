using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Saves.ModelBinders;
using PokeSaveRomManager.Api.Shared.ModelBinders;

namespace PokeSaveRomManager.Api.Saves.DTOs
{
    public class SaveFileDto
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
    }

    [ModelBinder(BinderType = typeof(SaveFileModelBinder))]
    public class SaveFileUploadDto
    {
        public IFormFile SaveFile { get; set; }

        [ModelBinder(BinderType = typeof(JsonModelBinder))]
        public SaveFileDto Metadata { get; set; }
    }
}
