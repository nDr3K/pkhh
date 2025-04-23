using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class Nature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? IncreasedStatId { get; set; }
        public int? DecreasedStatId { get; set; }

        // Navigation properties
        [ForeignKey("IncreasedStatId")]
        public Stat IncreasedStat { get; set; }
        [ForeignKey("DecreasedStatId")]
        public Stat DecreasedStat { get; set; }
        public ICollection<PokemonInstance> PokemonInstances { get; set; }
    }
}