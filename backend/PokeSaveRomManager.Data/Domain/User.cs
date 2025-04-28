using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
    [Index(nameof(Auth0Id), IsUnique = true)]
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