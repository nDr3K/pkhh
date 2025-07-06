using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Roms.DTOs;
using PokeSaveRomManager.Api.Types.DTOs;
using PokeSaveRomManager.Parser.Core.Models.Data;

namespace PokeSaveRomManager.Api.Roms.Mapper
{
    public static class RomDtosMapper
    {
        // Game
        public static GameCreateDto MapToGame(this RomSaveDto dto)
        {
            return new GameCreateDto
            {
                Name = dto.Name,
                Path = dto.Path,
                Generation = dto.Generation,
                Official = dto.Official,
                Region = dto.Region
            };
        }

        // Types
        public static IEnumerable<GameTypeCreateDto> MapToGameTypes(this IEnumerable<TypeData> typesData, IEnumerable<TypeDto> typesDto)
        {
            return typesData.Select(typeData =>
                new GameTypeCreateDto
                {
                    TypeId = typesDto.FirstOrDefault(t =>
                        string.Equals(t.Name, typeData.Name, StringComparison.OrdinalIgnoreCase)
                    )?.Id ?? throw new ArgumentNullException($"Type with name: {typeData.Name} is not recognized"),
                    TypeInGameId = typeData.Id
                }
            ).ToList();
        }

        // Category
        public static int MapToCategoryId(this Category category)
        {
            return category switch
            {
                Category.Physical => 1,
                Category.Special => 2,
                Category.Status => 3,
                _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
            };
        }

        // Rom
        public static RomSaveDto MapToRomSaveDto(this RomCreateDto dto, string path)
        {
            return new RomSaveDto
            {
                Name = dto.Name,
                Path = path,
                Offsets = dto.Offsets,
                Generation = dto.Generation,
                Official = dto.Official,
                Region = dto.Region,
            };
        }
    }
}
