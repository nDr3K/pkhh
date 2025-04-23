using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Generation { get; set; }
        public bool Official { get; set; } = true;
        public string Region { get; set; }

        // Navigation properties
        public ICollection<Pokemon> Pokemon { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Box> Boxes { get; set; }
        public ICollection<MoveGame> MoveGame { get; set; }
        public ICollection<TypeGame> TypeGame { get; set; }
    }
}