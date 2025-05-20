namespace PokeSaveRomManager.Api.Saves.Models
{
    public class SaveFileData
    {
        public List<PokemonData> Party { get; set; }
        public List<PokemonData> Boxes { get; set; }
    }
}
