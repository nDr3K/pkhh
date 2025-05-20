using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeSaveRomManager.Data.Domain
{
    public class Save
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } // Auth0Id

        [Required]
        public int GameId { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime LastUpdatedTime { get; set; }

        // Navigation properties

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public ICollection<SaveBox> Boxes { get; set; }
        public SaveTeam Party { get; set; }
        public ICollection<PokemonInstance> PokemonInstances { get; set; }
    }
}
