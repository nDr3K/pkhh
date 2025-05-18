using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class SaveTeamMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PartyId { get; set; }
        public int PokemonInstanceId { get; set; }

        // Navigation properties
        [ForeignKey("PartyId")]
        public SaveTeam Party { get; set; }

        [ForeignKey("PokemonInstanceId")]
        public PokemonInstance PokemonInstance { get; set; }
    }
}