using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Stats.DTOs
{
    public class StatDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class StatCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class StatUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
