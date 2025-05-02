using PokeSaveRomManager.Parser.Core.Exceptions;
using PokeSaveRomManager.Parser.Core.Parsers;
using PokeSaveRomManager.Parser.Gen1.RomParser;
using PokeSaveRomManager.Parser.Gen1.SaveParser;

namespace PokeSaveRomManager.Parser.Registry
{
    public class ParserFactory
    {
        private readonly IParserRegistry _registry;

        public ParserFactory(IParserRegistry registry)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));

            // Register default parsers
            RegisterDefaultParsers();
        }

        public IRomParser CreateRomParser(byte[] romData)
        {
            if (romData == null || romData.Length == 0)
                throw new ArgumentException("ROM data cannot be null or empty.", nameof(romData));

            var gameVersion = FileIdentifier.DetectRomGameVersion(romData);
            var parser = _registry.GetRomParser(gameVersion);
            if (parser == null)
                throw new UnsupportedRomException("Unknown or unsupported ROM version");

            return parser;
        }

        public ISaveParser CreateSaveParser(byte[] saveData)
        {
            if (saveData == null || saveData.Length == 0)
                throw new ArgumentException("Save data cannot be null or empty.", nameof(saveData));

            var gameVersion = FileIdentifier.DetectSaveGameVersion(saveData);
            var parser = _registry.GetSaveParser(gameVersion);
            if (parser == null)
                throw new UnsupportedSaveException($"Unsupported or unknown Save file.");

            return parser;
        }

        private void RegisterDefaultParsers()
        {
            // Register default parsers for ROMs
            _registry.RegisterRomParser(FileIdentifier.GameVersion.Gen1, new Gen1RomParser());

            // Register default parsers for saves
            _registry.RegisterSaveParser(FileIdentifier.GameVersion.Gen1, new Gen1SaveParser());
        }
    }
}