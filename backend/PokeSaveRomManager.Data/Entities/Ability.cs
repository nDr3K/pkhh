using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Entities
{
    public class Ability
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Navigation properties
        public ICollection<PokemonAbility> PokemonAbilities { get; set; }
    }
}