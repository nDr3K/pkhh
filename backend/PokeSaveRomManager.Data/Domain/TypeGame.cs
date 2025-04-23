using System.ComponentModel.DataAnnotations.Schema;

namespace PokeSaveRomManager.Data.Domain
{
    public class TypeGame
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int GameId { get; set; }

        // Navigation properties
        [ForeignKey("TypeId")]
        public Type Type { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
