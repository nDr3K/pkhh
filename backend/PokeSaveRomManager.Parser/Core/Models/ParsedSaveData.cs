using PokeSaveRomManager.Parser.Core.Models.Data;

namespace PokeSaveRomManager.Parser.Core.Models
{
    public class ParsedSaveData
    {
        public PlayerData PlayerData { get; set; }
        public List<BoxData> Boxes { get; set; }
        public List<PokemonSaveData> Party { get; set; }
    }
}