using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Entities
{
    public class Form
    {
        [Key]
        public int Id { get; set; }
        public int PokemonId { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsRegional { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
        public int? FormOrder { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }

        // Navigation properties
        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; }
        [ForeignKey("Type1Id")]
        public Type Type1 { get; set; }
        [ForeignKey("Type2Id")]
        public Type Type2 { get; set; }
        public ICollection<PokemonInstance> PokemonInstances { get; set; }
    }
}