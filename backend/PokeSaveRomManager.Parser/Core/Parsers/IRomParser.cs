using PokeSaveRomManager.Parser.Core.Models;

namespace PokeSaveRomManager.Parser.Core.Parsers
{
    public interface IRomParser : IGameParser
    {
        RomInfo ParseRomInfo(byte[] data);
        ParserResult<ParsedRomData> ExtractRomData(byte[] data, RomOffsetsMap offsetsMap);
    }
}
