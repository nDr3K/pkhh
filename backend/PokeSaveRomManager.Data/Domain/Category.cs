using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Navigation properties
        public ICollection<Move> Moves { get; set; }
    }
}