using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Registry;

namespace PokeSaveRomManager.Parser.Services
{
    public class ParserOrchestrator
    {
        private readonly ParserFactory _parserFactory;

        public ParserOrchestrator(ParserFactory parserFactory)
        {
            _parserFactory = parserFactory;
        }

        public ParserResult<ParsedRomData> ParseRom(byte[] romData, RomOffsetsMap offsets)
        {
            var parser = _parserFactory.CreateRomParser(romData);
            return parser.ExtractRomData(romData, offsets);
        }

        public ParserResult<ParsedSaveData> ParseSave(byte[] saveData)
        {
            var parser = _parserFactory.CreateSaveParser(saveData);
            return parser.ExtractSaveData(saveData);
        }
    }
}