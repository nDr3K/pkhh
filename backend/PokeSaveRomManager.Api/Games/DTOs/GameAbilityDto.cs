using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Games.DTOs
{
    public class GameAbilityDto
    {
        public int Id { get; set; }

        [Required]
        public string GameName { get; set; }

        [Required]
        public string AbilityName { get; set; }

        [Required]
        public int AbilityInGameId { get; set; }
    }

    public class GameAbilityCreateDto
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public int AbilityId { get; set; }

        [Required]
        public int AbilityInGameId { get; set; }
    }

    public class GameAbilityUpdateDto
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public int AbilityId { get; set; }

        [Required]
        public int AbilityInGameId { get; set; }
    }
}
