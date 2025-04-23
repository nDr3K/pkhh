using PokeSaveRomManager.Api.Natures.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Natures.Mapper
{
    public static class NatureMapper
    {
        public static NatureDto ToDto(this Nature nature)
        {
            if (nature == null) 
                return null;

            return new NatureDto
            {
                Id = nature.Id,
                Name = nature.Name,
                IncreasedStat = nature.IncreasedStat?.Name,
                DecreasedStat = nature.DecreasedStat?.Name
            };
        }

        public static IEnumerable<NatureDto> ToDtos(this IEnumerable<Nature> natures)
        {
            return natures.Select(ToDto);
        }

        public static Nature ToEntity(this NatureCreateDto dto)
        {
            if (dto == null) 
                return null;

            return new Nature
            {
                Name = dto.Name,
                IncreasedStatId = dto.IncreasedStatId,
                DecreasedStatId = dto.DecreasedStatId
            };
        }

        public static void UpdateFromDto(this Nature nature, NatureUpdateDto dto)
        {
            if (nature == null || dto == null) 
                return;

            nature.Name = dto.Name;
            nature.IncreasedStatId = dto.IncreasedStatId;
            nature.DecreasedStatId = dto.DecreasedStatId;
        }
    }
}
