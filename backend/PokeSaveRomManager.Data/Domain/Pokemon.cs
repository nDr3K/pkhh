using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class Pokemon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DexNumber { get; set; }
        public int GameId { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int? Special { get; set; }
        public int? SpAttack { get; set; }
        public int? SpDefense { get; set; }
        public int Speed { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }
        public string FormName { get; set; }
        public bool IsRegionalForm { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;

        // Navigation properties
        [ForeignKey("GameId")]
        public Game Game { get; set; }
        [ForeignKey("Type1Id")]
        public Type Type1 { get; set; }
        [ForeignKey("Type2Id")]
        public Type Type2 { get; set; }
        public ICollection<Form> Forms { get; set; }
        public ICollection<PokemonAbility> Abilities { get; set; }
        public ICollection<MoveLearning> MoveLearning { get; set; }
        public ICollection<PokemonInstance> Instances { get; set; }
    }
}