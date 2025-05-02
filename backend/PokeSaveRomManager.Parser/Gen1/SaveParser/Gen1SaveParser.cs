using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Parsers;

namespace PokeSaveRomManager.Parser.Gen1.SaveParser
{
    public class Gen1SaveParser : ISaveParser
    {
        public int Generation => throw new NotImplementedException();

        public bool CanParse(byte[] fileData)
        {
            throw new NotImplementedException();
        }

        public ParserResult<ParsedSaveData> ExtractSaveData(byte[] data)
        {
            throw new NotImplementedException();
        }

        public bool ValidateSavefile(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}