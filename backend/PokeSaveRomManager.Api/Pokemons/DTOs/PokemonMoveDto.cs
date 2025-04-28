namespace PokeSaveRomManager.Api.Pokemons.DTOs
{
    public class PokemonMoveDto
    {
        public int Id { get; set; }
        public string PokemonName { get; set; }
        public string GameInMoveName { get; set; }
        public string MethodName { get; set; }
        public int? Level { get; set; }
        public string TMNumber { get; set; }
        public bool IsTutor { get; set; } = false;
        public bool IsEggMove { get; set; } = false;
    }

    public class PokemonMoveCreateDto
    {
        public int GameInMoveId { get; set; }
        public int MethodId { get; set; }
        public int? Level { get; set; }
        public string TMNumber { get; set; }
        public bool IsTutor { get; set; } = false;
        public bool IsEggMove { get; set; } = false;
    }

    public class PokemonMoveUpdateDto
    {
        public int GameInMoveId { get; set; }
        public int MethodId { get; set; }
        public int? Level { get; set; }
        public string TMNumber { get; set; }
        public bool IsTutor { get; set; } = false;
        public bool IsEggMove { get; set; } = false;
    }
}
