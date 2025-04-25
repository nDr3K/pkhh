using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Abilities.DTOs
{
    public class AbilityNameDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class AbilityNameCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class AbilityNameUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
