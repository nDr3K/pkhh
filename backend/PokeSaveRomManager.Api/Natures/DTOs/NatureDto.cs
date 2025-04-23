using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Natures.DTOs
{
    public class NatureDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string IncreasedStat { get; set; }
        public string DecreasedStat { get; set; }
    }

    public class NatureCreateDto
    {
        [Required]
        public string Name { get; set; }
        public int? IncreasedStatId { get; set; }
        public int? DecreasedStatId { get; set; }
    }

    public class NatureUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public int? IncreasedStatId { get; set; }
        public int? DecreasedStatId { get; set; }
    }
}
