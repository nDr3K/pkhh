using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class Pokemon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DexNumber { get; set; }
        public int GameId { get; set; }

        // Navigation properties
        [ForeignKey("GameId")]
        public Game Game { get; set; }
        public ICollection<PokemonForm> Forms { get; set; }
        public ICollection<PokemonAbility> Abilities { get; set; }
        public ICollection<PokemonMove> Moves { get; set; }
        public ICollection<PokemonInstance> Instances { get; set; }
    }
}