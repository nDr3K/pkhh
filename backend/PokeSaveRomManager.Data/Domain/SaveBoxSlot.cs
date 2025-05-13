using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class SaveBoxSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BoxId { get; set; }
        public int PokemonInstanceId { get; set; }
        public int SlotNumber { get; set; }

        // Navigation properties
        [ForeignKey("BoxId")]
        public SaveBox Box { get; set; }
        [ForeignKey("PokemonInstanceId")]
        public PokemonInstance PokemonInstance { get; set; }
    }
}