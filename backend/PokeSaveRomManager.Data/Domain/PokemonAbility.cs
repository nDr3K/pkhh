using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class PokemonAbility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int AbilityId { get; set; }
        public bool IsHidden { get; set; } = false;
        public int AbilitySlot { get; set; }

        // Navigation properties
        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; }
        [ForeignKey("AbilityId")]
        public Ability Ability { get; set; }
    }
}