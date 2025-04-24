using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Items.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class ItemCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public class ItemUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
