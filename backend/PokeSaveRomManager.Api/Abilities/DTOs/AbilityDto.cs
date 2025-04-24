using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Abilities.DTOs
{
    public class AbilityDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class AbilityCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class AbilityUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
