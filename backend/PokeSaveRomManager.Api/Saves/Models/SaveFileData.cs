namespace PokeSaveRomManager.Api.Saves.Models
{
    public class SaveFileData
    {
        public List<PokemonData> Party { get; set; }
        public List<BoxData> Boxes { get; set; }
    }
}
