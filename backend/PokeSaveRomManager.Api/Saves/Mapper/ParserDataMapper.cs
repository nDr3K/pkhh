using PokeSaveRomManager.Api.Saves.Models;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Saves.Mapper
{
    public static class ParserDataMapper
    {
        public static PokemonInstance ToDomain(this PokemonData data, int saveId)
        {
            return new PokemonInstance
            {
                SaveId = saveId,
                PokemonId = data.PokemonId,
                FormId = data.FormId,
                Nickname = "", // Not tracked for now
                Gender = GenderType.Genderless, // Not tracked for now
                Level = data.Level,
                Shiny = false, // Not tracked for now
                NatureId = null, // TODO: Gen3+
                HeldItemId = null,
                AbilityId = data.AbilityId,
                Move1Id = data.Move1Id,
                Move2Id = data.Move2Id,
                Move3Id = data.Move3Id,
                Move4Id = data.Move4Id,
                IVHP = data.HPIV,
                IVAttack = data.AttackIV,
                IVDefense = data.DefenseIV,
                IVSpecial = data?.SpecialIV,
                IVSpAttack = data?.SpecialAttackIV,
                IVSpDefense = data?.SpecialDefenseIV,
                IVSpeed = data.SpeedIV,
                EVHP = data?.HPEV,
                EVAttack = data?.AttackEV,
                EVDefense = data?.DefenseEV,
                EVSpecial = data?.SpecialEV,
                EVSpAttack = data?.SpecialAttackEV,
                EVSpDefense = data?.SpecialDefenseEV,
                EVSpeed = data?.SpeedEV,
            };
        }

        public static IEnumerable<PokemonInstance> ToDomain(this IEnumerable<PokemonData> data, int saveId)
        {
            return data.Select(p => p.ToDomain(saveId));
        }
    }
}
