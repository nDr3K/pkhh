using PokeSaveRomManager.Parser.Core.Models;

namespace PokeSaveRomManager.Parser.Core.Parsers
{
    public interface ISaveParser : IGameParser
    {
        ParserResult<ParsedSaveData> ExtractSaveData(byte[] data);
        bool ValidateSavefile(byte[] data);
    }
}