namespace PokeSaveRomManager.Parser.Core.Models.Data
{
    public class BoxData
    {
        public string Name { get; set; }
        public int BoxIndex { get; set; }
        public int PokemonCount { get; set; }
        public int MaxPokemonCount { get; set; }
        public List<PokemonSaveData> Pokemon { get; set; }
    }
}