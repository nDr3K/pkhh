using PokeSaveRomManager.Api.Items.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Items.Mapper
{
    public static class ItemMapper
    {
        public static ItemDto ToDto(this Item Item)
        {
            return new ItemDto
            {
                Id = Item.Id,
                Name = Item.Name
            };
        }
        public static IEnumerable<ItemDto> ToDtos(this IEnumerable<Item> Items)
        {
            return Items.Select(c => c.ToDto());
        }

        public static Item ToEntity(this ItemCreateDto ItemyCreateDto)
        {
            if (ItemyCreateDto == null)
                return null;

            return new Item
            {
                Name = ItemyCreateDto.Name
            };
        }

        public static void UpdateFromDto(this Item Item, ItemUpdateDto ItemUpdateDto)
        {
            if (Item == null || ItemUpdateDto == null)
                return;

            Item.Name = ItemUpdateDto.Name;
        }
    }
}
