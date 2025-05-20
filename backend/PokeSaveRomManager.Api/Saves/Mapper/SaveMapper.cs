using PokeSaveRomManager.Api.Saves.DTOs;
using PokeSaveRomManager.Api.Saves.Models;
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
                Game = save.Game.Name,
                Name = save.Name,
                Team = save.Party.Members.Select(m => new SaveDtoPokemon
                {
                    Id = m.PokemonInstance.Id,
                    Name = m.PokemonInstance.Pokemon.Name
                }).ToArray(),
                LastUpdatedTime = save.LastUpdatedTime

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
                Game = save.Game.Name,
                Team = save.Party.Members.Select(m => m.PokemonInstance.ToSaveDetailDtoPokemon()).ToArray(),
                Boxes = save.Boxes.SelectMany(b => b.Slots).Select(s => s.PokemonInstance.ToSaveDetailDtoPokemon()).ToArray(),
                LastUpdatedTime = save.LastUpdatedTime
            };
        }

        private static SaveDetailDtoPokemon ToSaveDetailDtoPokemon(this PokemonInstance pokemonInstance)
        {
            return new SaveDetailDtoPokemon
            {
                Id = pokemonInstance.Id,
                Name = pokemonInstance.Pokemon.Name,
                Type1 = pokemonInstance.Form.Type1.Name,
                Type2 = pokemonInstance.Form.Type2?.Name,
                Ability = pokemonInstance.Ability?.Name?.Name,
                Level = pokemonInstance.Level,
                Nature = pokemonInstance.Nature?.Name,
                Move1 = pokemonInstance.Move1?.Name?.Name,
                Move2 = pokemonInstance.Move2?.Name?.Name,
                Move3 = pokemonInstance.Move3?.Name?.Name,
                Move4 = pokemonInstance.Move4?.Name?.Name
            };
        }

        public static IEnumerable<SaveDetailDto> ToDetailDtos(this IEnumerable<Save> saves)
        {
            return saves.Select(s => s.ToDetailDto());
        }
        #endregion

        // SaveFile
        #region SaveFile
        public static Save ToDomain(this SaveFileData saveFileData, int userId, SaveFileDto saveFileDto)
        {
            var save = new Save
            {
                UserId = userId,
                GameId = saveFileDto.GameId,
                Name = saveFileDto.Name,
                Boxes = new List<SaveBox>(),
                LastUpdatedTime = DateTime.UtcNow,
            };

            var party = new SaveTeam
            {
                Members = new List<SaveTeamMember>()
            };

            save.Party = party;

            var box = CreateSaveBox(save.Id);

            save.Boxes.Add(box);

            return save;
        }

        public static SaveBox CreateSaveBox(int saveId)
        {
            return new SaveBox
            {
                Name = "Box", // Default name,
                SaveId = saveId,
                Slots = new List<SaveBoxSlot>()
            };
        }
        #endregion
    }
}
