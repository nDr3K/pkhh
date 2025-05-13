using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class PokemonForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int? InternalId { get; set; }

        [Required]
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int? Special { get; set; }
        public int? SpAttack { get; set; }
        public int? SpDefense { get; set; }
        public int Speed { get; set; }
        public bool IsDefault { get; set; } = false;
        public bool IsRegional { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
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