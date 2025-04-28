namespace PokeSaveRomManager.Api.Pokemons.DTOs
{
    public class PokemonAbilityDto
    {
        public int Id { get; set; }
        public string PokemonName { get; set; }
        public string AbilityName { get; set; }
        public bool IsHidden { get; set; } = false;
        public int AbilitySlot { get; set; }
    }

    public class PokemonAbilityCreateDto
    {
        public int AbilityId { get; set; }
        public bool IsHidden { get; set; } = false;
        public int AbilitySlot { get; set; }
    }

    public class PokemonAbilityUpdateDto
    {
        public int AbilityId { get; set; }
        public bool IsHidden { get; set; } = false;
        public int AbilitySlot { get; set; }
    }
}
