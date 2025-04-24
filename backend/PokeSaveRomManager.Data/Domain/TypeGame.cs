using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeSaveRomManager.Data.Domain
{
    public class TypeGame
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int GameTypeId { get; set; } // id of the type in the game

        // Navigation properties
        [ForeignKey("TypeId")]
        public Type Type { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
