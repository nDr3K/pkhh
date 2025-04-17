using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
        public ICollection<TeamMember> Members { get; set; }
    }
}