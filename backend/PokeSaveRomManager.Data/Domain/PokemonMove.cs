using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class PokemonMove
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int GameInMoveId { get; set; }
        public int MethodId { get; set; }
        public int? Level { get; set; }
        public string TMNumber { get; set; }
        public bool IsTutor { get; set; } = false;
        public bool IsEggMove { get; set; } = false;

        // Navigation properties
        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; }
        [ForeignKey("GameInMoveId")]
        public GameMove GameMove { get; set; }
        [ForeignKey("MethodId")]
        public MoveLearningMethod Method { get; set; }
    }
}