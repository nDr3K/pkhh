using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeSaveRomManager.Data.Domain
{
    public class GameAbility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AbilityId { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public int AbilityInGameId { get; set; } // Unique identifier for the ability in the game


        // Navigation properties
        [ForeignKey("AbilityId")]
        public Ability Ability { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
