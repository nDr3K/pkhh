using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeSaveRomManager.Data.Domain
{
    public class MoveGame
    {
        [Key]
        public int Id { get; set; }
        public int MoveId { get; set; }
        public int GameId { get; set; }
        public int MoveGameId { get; set; } // Unique identifier for the move in the game

        [ForeignKey("MoveId")]
        public Move Move { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
