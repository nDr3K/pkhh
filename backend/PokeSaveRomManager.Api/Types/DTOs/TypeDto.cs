using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Types.DTOs
{
    public class TypeDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class TypeCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class TypeUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
