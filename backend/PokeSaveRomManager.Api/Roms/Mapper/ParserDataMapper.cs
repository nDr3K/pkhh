using PokeSaveRomManager.Api.Roms.Models;
using PokeSaveRomManager.Parser.Core.Models.Data;

namespace PokeSaveRomManager.Api.Roms.Mapper
{
    public static class ParserDataMapper
    {
        public static IEnumerable<PokemonData> MapPokemonData(IEnumerable<PokemonStatsData> statsData, IEnumerable<PokemonNameData> nameDatas)
        {
            var pokemonDataList = new List<PokemonData>();
            foreach (var stats in statsData)
            {
                var name = nameDatas.FirstOrDefault(n => n.InternalId == stats.InternalId);
                if (name == null) continue;
                var pokemonData = new PokemonData
                {
                    InternalId = stats.InternalId,
                    Name = name.Name,
                    DexNumber = stats.DexNumber,
                    BaseHP = stats.BaseHP,
                    BaseAttack = stats.BaseAttack,
                    BaseDefense = stats.BaseDefense,
                    BaseSpecial = stats.BaseSpecial,
                    BaseSpAttack = stats.BaseSpAttack,
                    BaseSpDefense = stats.BaseSpDefense,
                    BaseSpeed = stats.BaseSpeed,
                    Type1Id = stats.Type1Id,
                    Type2Id = stats.Type2Id,
                    CatchRate = stats.CatchRate,
                    BaseExpYield = stats.BaseExpYield,
                    GrowthRate = stats.GrowthRate
                };
                pokemonDataList.Add(pokemonData);
            }
            return pokemonDataList;
        }
    }
}
