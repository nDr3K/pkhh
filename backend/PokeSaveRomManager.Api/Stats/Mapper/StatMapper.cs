using PokeSaveRomManager.Api.Stats.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Stats.Mapper
{
    public static class StatMapper
    {
        public static StatDto ToDto(this Stat stat)
        {
            return new StatDto
            {
                Id = stat.Id,
                Name = stat.Name,
            };
        }
        public static IEnumerable<StatDto> ToDtos(this IEnumerable<Stat> stats)
        {
            return stats.Select(s => s.ToDto());
        }
        public static Stat ToEntity(this StatCreateDto statDto)
        {
            if (statDto == null)
                return null;
            return new Stat
            {
                Name = statDto.Name,
            };
        }
        public static void UpdateFromDto(this Stat stat, StatUpdateDto statDto)
        {
            if (stat == null || statDto == null)
                return;
            stat.Name = statDto.Name;
        }
    }
}
