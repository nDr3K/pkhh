using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Entities
{
    public class Box
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
        public ICollection<BoxSlot> Slots { get; set; }
    }
}