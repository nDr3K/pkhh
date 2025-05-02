using PokeSaveRomManager.Parser.Core.Models.Data;

namespace PokeSaveRomManager.Parser.Core.Models
{
    public class ParsedRomData
    {
        public List<MoveData> Moves { get; set; }
        public List<MoveName> MoveNames { get; set; }
        public List<PokemonStatsData> PokemonStats { get; set; }
        public List<PokemonNameData> PokemonNames { get; set; }
        public List<TypeData> Types { get; set; }

    }
}