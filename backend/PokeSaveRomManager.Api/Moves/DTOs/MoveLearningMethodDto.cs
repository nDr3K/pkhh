using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Moves.DTOs
{
    public class MoveLearningMethodDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class MoveLearningMethodCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class MoveLearningMethodUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
