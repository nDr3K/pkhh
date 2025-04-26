using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Moves.DTOs
{
    // Move
    public class MoveDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Category { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int? PP { get; set; }
        public string Effect { get; set; }
        public int Priority { get; set; }
    }

    public class MoveCreateDto
    {
        [Required]
        public int NameId { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int? PP { get; set; }
        public string Effect { get; set; }
        public int Priority { get; set; }
    }

    public class MoveUpdateDto
    {
        [Required]
        public int NameId { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int? PP { get; set; }
        public string Effect { get; set; }
        public int Priority { get; set; }
    }

    // MoveName
    public class MoveNameDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class MoveNameCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class MoveNameUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}