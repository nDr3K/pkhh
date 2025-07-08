using PokeSaveRomManager.Api.Saves.DTOs;
using PokeSaveRomManager.Data.Domain;
using PokeSaveRomManager.Parser.Core.Models.Data;
using BoxData = PokeSaveRomManager.Api.Saves.Models.BoxData;

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
                    DexNumber = m.PokemonInstance.Pokemon.DexNumber,
                    Name = m.PokemonInstance.Pokemon.Name
                }).ToArray(),
                CreatedAt = save.CreatedAt,
                UpdatedAt = save.UpdatedAt

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
                Name = save.Name,
                Path = save.Path,
                Description = save.Description,
                Tags = save.Tags?.Split(',').Select(t => t.Trim()).Where(t => !string.IsNullOrWhiteSpace(t)).ToArray() ?? Array.Empty<string>(),
                PlayTime = save.PlayTime,
                Badges = save.Badges.ToListBadges(),
                PokemonSeen = save.PokemonSeen,
                PokemonCaught = save.PokemonCaught,
                PokemonTotal = save.PokemonTotal,
                IsFavorite = save.IsFavorite,
                PlayerName = save.PlayerName,
                Team = save.Party.Members.Select(m => m.PokemonInstance.ToSaveDetailDtoPokemon()).ToArray(),
                Boxes = save.Boxes.Select(box => new SaveDetailDtoBox
                {
                    Id = box.Id,
                    Name = box.Name,
                    Capacity = box.Capacity,
                    Count = box.Count,
                    Pokemons = box.Slots
                        .OrderBy(s => s.SlotNumber)
                        .Select(s => s.PokemonInstance.ToSaveDetailDtoPokemon())
                        .ToArray()
                }).ToArray(),
                CreatedAt = save.CreatedAt,
                UpdatedAt = save.UpdatedAt
            };
        }

        private static SaveDetailDtoPokemon ToSaveDetailDtoPokemon(this PokemonInstance pokemonInstance)
        {
            return new SaveDetailDtoPokemon
            {
                Id = pokemonInstance.Id,
                DexNumber = pokemonInstance.Pokemon.DexNumber,
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
        public static Save ToDomain(this PlayerData playerData, int userId, SaveFileDto saveFileDto, string path)
        {
            return new Save
            {
                UserId = userId,
                GameId = saveFileDto.GameId,
                Name = saveFileDto.Name,
                Path = path,
                Description = saveFileDto.Description,
                Tags = saveFileDto.Tags != null ? string.Join(",", saveFileDto.Tags) : string.Empty,
                PlayTime = playerData.GameTime.ToString(),
                Badges = playerData.Badges.ToByteBadges(),
                PokemonSeen = playerData.Pokedex.Seen,
                PokemonCaught = playerData.Pokedex.Owned,
                PokemonTotal = playerData.Pokedex.Total,
                IsFavorite = false,
                PlayerName = playerData.Name,
                Party = new SaveTeam
                {
                    Members = new List<SaveTeamMember>()
                },
                Boxes = new List<SaveBox>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static SaveBox CreateSaveBox(int saveId, BoxData boxData)
        {
            return new SaveBox
            {
                Name = "Box", // Default name,
                SaveId = saveId,
                Capacity = boxData.Capacity,
                Count = boxData.Slots.Count,
                Slots = new List<SaveBoxSlot>()
            };
        }

        private static Badges ToByteBadges(this List<Badge> badges)
        {
            var badge = new Badges();
            foreach (var b in badges)
            {
                switch(b.Index)
                {
                    case 1: badge |= Badges.Badge1; break;
                    case 2: badge |= Badges.Badge2; break;
                    case 3: badge |= Badges.Badge3; break;
                    case 4: badge |= Badges.Badge4; break;
                    case 5: badge |= Badges.Badge5; break;
                    case 6: badge |= Badges.Badge6; break;
                    case 7: badge |= Badges.Badge7; break;
                    case 8: badge |= Badges.Badge8; break;
                    default: throw new ArgumentOutOfRangeException(nameof(b.Index), $"Invalid badge index: {b.Index}");
                }
            }
            return badge;
        }

        public static int[] ToListBadges(this Badges badgeFlags)
        {
            var badges = new List<int>();

            foreach (Badges badge in Enum.GetValues(typeof(Badges)))
            {
                if (badge == Badges.None) continue;

                if (badgeFlags.HasFlag(badge))
                {
                    int index = (int)Math.Log((byte)badge, 2) + 1;
                    badges.Add(index);
                }
            }

            return badges.ToArray();
        }
        #endregion
    }
}
