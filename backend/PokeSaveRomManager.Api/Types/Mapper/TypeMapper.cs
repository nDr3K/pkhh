using PokeSaveRomManager.Api.Types.DTOs;
using Type = PokeSaveRomManager.Data.Domain.Type;

namespace PokeSaveRomManager.Api.Types.Mapper
{
    public static class TypeMapper
    {
        public static TypeDto ToDto(this Type type)
        {
            return new TypeDto
            {
                Id = type.Id,
                Name = type.Name,
            };
        }

        public static IEnumerable<TypeDto> ToDtos(this IEnumerable<Type> types)
        {
            return types.Select(t => t.ToDto());
        }

        public static Type ToEntity(this TypeCreateDto typeDto)
        {
            if (typeDto == null)
                return null;

            return new Type
            {
                Name = typeDto.Name,
            };
        }

        public static void UpdateFromDto(this Type type, TypeUpdateDto typeDto)
        {
            if (type == null || typeDto == null)
                return;
            type.Name = typeDto.Name;
        }
    }
}
