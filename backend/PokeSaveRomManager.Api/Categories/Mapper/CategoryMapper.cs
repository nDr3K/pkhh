using PokeSaveRomManager.Api.Categories.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Categories.Mapper
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public static IEnumerable<CategoryDto> ToDtos(this IEnumerable<Category> categories)
        {
            return categories.Select(c => c.ToDto());
        }

        public static Category ToEntity(this CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null)
                return null;

            return new Category
            {
                Name = categoryCreateDto.Name
            };
        }

        public static void UpdateFromDto(this Category category, CategoryUpdateDto categoryUpdateDto)
        {
            if (category == null || categoryUpdateDto == null)
                return;

            category.Name = categoryUpdateDto.Name;
        }

    }
}
