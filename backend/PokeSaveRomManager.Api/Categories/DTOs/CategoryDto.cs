using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Categories.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class CategoryUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
