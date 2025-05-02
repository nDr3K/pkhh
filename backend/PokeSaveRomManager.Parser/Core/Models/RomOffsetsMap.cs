namespace PokeSaveRomManager.Parser.Core.Models
{
    public class RomOffsetsMap
    {
        public DataSection Moves { get; set; }
        public DataSection MoveNames { get; set; }
        public DataSection PokemonStats { get; set; }
        public DataSection PokemonNames { get; set; }
        public DataSection Pokedex { get; set; }
        public DataSection Types { get; set; }
    }

    public class DataSection
    {
        public int Offset { get; set; }
        public int EntryLength { get; set; } // If 0, it means it uses terminator
        public int Count { get; set; }
    }
}
