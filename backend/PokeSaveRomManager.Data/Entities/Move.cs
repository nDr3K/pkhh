using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Entities
{
    public class Move
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int? PP { get; set; }
        public string Effect { get; set; }
        public int Priority { get; set; } = 0;

        // Navigation properties
        [ForeignKey("TypeId")]
        public Type Type { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<MoveGame> MoveGame { get; set; }
        public ICollection<PokemonInstance> PokemonInstancesWithMove1 { get; set; }
        public ICollection<PokemonInstance> PokemonInstancesWithMove2 { get; set; }
        public ICollection<PokemonInstance> PokemonInstancesWithMove3 { get; set; }
        public ICollection<PokemonInstance> PokemonInstancesWithMove4 { get; set; }
    }
}