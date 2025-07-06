using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Roms.ModelBinders;
using PokeSaveRomManager.Api.Shared.ModelBinders;
using PokeSaveRomManager.Parser.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Roms.DTOs
{
    public class RomCreateDto
    {
        [Required]
        public RomOffsetsMap Offsets { get; set; }
        public string Name { get; set; }
        public int Generation { get; set; }
        public bool Official { get; set; }
        public string Region { get; set; }
    }

    public class RomSaveDto
    {
        [Required]
        public RomOffsetsMap Offsets { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int Generation { get; set; }
        public bool Official { get; set; }
        public string Region { get; set; }
    }

    [ModelBinder(BinderType = typeof(RomModelBinder))]
    public class RomUploadDto
    {
        [Required]
        public IFormFile RomFile { get; set; }

        [Required]
        [ModelBinder(BinderType = typeof(JsonModelBinder))]
        public RomCreateDto Metadata { get; set; }
    }
}
