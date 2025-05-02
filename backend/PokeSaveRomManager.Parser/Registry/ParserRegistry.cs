using PokeSaveRomManager.Parser.Core.Parsers;

namespace PokeSaveRomManager.Parser.Registry
{
    public class ParserRegistry : IParserRegistry
    {
        private readonly Dictionary<string, IRomParser> _romParsers = [];
        private readonly Dictionary<string, ISaveParser> _saveParsers = [];

        public void RegisterRomParser(string gameVersion, IRomParser parser)
        {
            if (string.IsNullOrWhiteSpace(gameVersion))
                throw new ArgumentNullException(nameof(gameVersion));

            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            _romParsers[gameVersion.ToLowerInvariant()] = parser;
        }

        public void RegisterSaveParser(string gameVersion, ISaveParser parser)
        {
            if (string.IsNullOrWhiteSpace(gameVersion))
                throw new ArgumentNullException(nameof(gameVersion));

            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            _saveParsers[gameVersion.ToLowerInvariant()] = parser;
        }

        public IRomParser GetRomParser(string gameVersion)
        {
            if (string.IsNullOrWhiteSpace(gameVersion))
                throw new ArgumentNullException(nameof(gameVersion));

            return _romParsers.TryGetValue(gameVersion.ToLowerInvariant(), out var parser) ? parser : null;
        }

        public ISaveParser GetSaveParser(string gameVersion)
        {
            if (string.IsNullOrWhiteSpace(gameVersion))
                throw new ArgumentNullException(nameof(gameVersion));

            return _saveParsers.TryGetValue(gameVersion.ToLowerInvariant(), out var parser) ? parser : null;
        }

        public IEnumerable<IRomParser> GetRomParsers()
        {
            return _romParsers.Values;
        }

        public IEnumerable<ISaveParser> GetSaveParsers()
        {
            return _saveParsers.Values;
        }

        public bool HasRomParser(string gameVersion)
        {
            if (string.IsNullOrWhiteSpace(gameVersion))
                return false;

            return _romParsers.ContainsKey(gameVersion.ToLowerInvariant());
        }

        public bool HasSaveParser(string gameVersion)
        {
            if (string.IsNullOrWhiteSpace(gameVersion))
                return false;

            return _saveParsers.ContainsKey(gameVersion.ToLowerInvariant());
        }
    }
}