using PokeSaveRomManager.Api.Saves.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Saves.Mapper
{
    public static class SaveMapper
    {
        // Save
        #region Save
        public static SaveDto ToDto(this Save save)
        {
            return new SaveDto
            {
                Id = save.Id,
                GameName = save.Game.Name,
                Team = save.Team.Members.Select(m => new SaveDtoPokemon
                {
                    Id = m.PokemonInstance.Id,
                    PokemonId = m.PokemonInstance.PokemonId
                }).ToArray()

            };
        }
        public static IEnumerable<SaveDto> ToDtos(this IEnumerable<Save> saves)
        {
            return saves.Select(s => s.ToDto());
        }
        #endregion

        // SaveDetail
        #region SaveDetail
        public static SaveDetailDto ToDetailDto(this Save save)
        {
            return new SaveDetailDto
            {
                Id = save.Id,
                GameName = save.Game.Name,
                Team = save.Team.Members.Select(m => m.PokemonInstance.ToSaveDetailDtoPokemon()).ToArray(),
                Boxes = save.Boxes.SelectMany(b => b.Slots).Select(s => s.PokemonInstance.ToSaveDetailDtoPokemon()).ToArray()
            };
        }

        private static SaveDetailDtoPokemon ToSaveDetailDtoPokemon(this PokemonInstance pokemonInstance)
        {
            return new SaveDetailDtoPokemon
            {
                Id = pokemonInstance.Id,
                PokemonName = pokemonInstance.Pokemon.Name,
                Type1 = pokemonInstance.Pokemon.Type1.Name,
                Type2 = pokemonInstance.Pokemon.Type2?.Name,
                Ability = pokemonInstance.Ability?.Name.Name,
                Level = pokemonInstance.Level,
                Nature = pokemonInstance.Nature?.Name,
                Move1 = pokemonInstance.Move1?.Name.Name,
                Move2 = pokemonInstance.Move2?.Name.Name,
                Move3 = pokemonInstance.Move3?.Name.Name,
                Move4 = pokemonInstance.Move4?.Name.Name
            };
        }

        public static IEnumerable<SaveDetailDto> ToDetailDtos(this IEnumerable<Save> saves)
        {
            return saves.Select(s => s.ToDetailDto());
        }
        #endregion
    }
}
