using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Entities
{
    public class MoveLearning
    {
        [Key]
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int MoveGameId { get; set; }
        public int MethodId { get; set; }
        public int? Level { get; set; }
        public string TMNumber { get; set; }
        public bool IsTutor { get; set; } = false;
        public bool IsEggMove { get; set; } = false;

        // Navigation properties
        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; }
        [ForeignKey("MoveGameId")]
        public MoveGame MoveGame { get; set; }
        [ForeignKey("MethodId")]
        public MoveLearningMethod Method { get; set; }
    }
}