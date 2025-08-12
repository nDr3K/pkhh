using PokeSaveRomManager.Api.Roms.Models;
using PokeSaveRomManager.Parser.Core.Models.Data;

namespace PokeSaveRomManager.Api.Roms.Mapper
{
    public static class ParserDataMapper
    {
        public static (IEnumerable<PokemonData>, IEnumerable<PartialPokemonData>) MapPokemonData(IEnumerable<PokemonStatsData> statsData, IEnumerable<PokemonNameData> nameDatas)
        {
            var partialPokemonDataList = new List<PartialPokemonData>();
            var pokemonDataList = new List<PokemonData>();
            foreach (var name in nameDatas)
            {
                var stats = statsData.FirstOrDefault(s => s.InternalId == name.InternalId);
                if (stats == null)
                {
                    var partialPokemonData = new PartialPokemonData
                    {
                        InternalId = name.InternalId,
                        Name = name.Name,
                        DexNumber = name.DexNumber,
                    };
                    partialPokemonDataList.Add(partialPokemonData);
                }
                else
                {
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
            }
            return (pokemonDataList, partialPokemonDataList);
        }
    }
}
