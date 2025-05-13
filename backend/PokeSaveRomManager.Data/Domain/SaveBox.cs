using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class SaveBox
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SaveId { get; set; }
        public string Name { get; set; }

        // Navigation properties
        [ForeignKey("SaveId")]
        public Save Save { get; set; }

        public ICollection<SaveBoxSlot> Slots { get; set; }
    }
}