namespace PokeSaveRomManager.Api.Saves.Models
{
    public class BoxData
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<PokemonData> Slots { get; set; }
    }
}
