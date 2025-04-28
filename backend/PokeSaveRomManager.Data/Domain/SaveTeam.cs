using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class SaveTeam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SaveId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("SaveId")]
        public Save Save { get; set; }

        public ICollection<SaveTeamMember> Members { get; set; }
    }
}