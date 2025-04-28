using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Auth0Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}