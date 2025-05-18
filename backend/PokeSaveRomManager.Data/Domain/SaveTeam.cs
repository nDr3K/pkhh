using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class SaveTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int SaveId { get; set; }

        // Navigation properties
        [ForeignKey("SaveId")]
        public Save Save { get; set; }

        public ICollection<SaveTeamMember> Members { get; set; }
    }
}