using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PokeSaveRomManager.Data.Entities
{
    public class Stat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Navigation properties
        public ICollection<Nature> IncreasedNatures { get; set; }
        public ICollection<Nature> DecreasedNatures { get; set; }
    }
}