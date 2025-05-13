using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeSaveRomManager.Data.Domain
{
    public class Ability
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int NameId { get; set; }

        // Navigation properties
        [ForeignKey("NameId")]
        public AbilityName Name { get; set; }
        public ICollection<PokemonAbility> PokemonAbilities { get; set; }
        public ICollection<GameAbility> GameAbilities { get; set; }
    }
}