using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeSaveRomManager.Data.Domain
{
    public class GameMove
    {
        [Key]
        public int Id { get; set; }
        public int MoveId { get; set; }
        public int GameId { get; set; }
        public int MoveInGameId { get; set; } // Unique identifier for the move in the game

        // Navigation properties
        [ForeignKey("MoveId")]
        public Move Move { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
