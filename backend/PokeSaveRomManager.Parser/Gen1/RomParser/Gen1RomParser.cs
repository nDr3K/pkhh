using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Parsers;
using PokeSaveRomManager.Parser.Core.Utils;

namespace PokeSaveRomManager.Parser.Gen1.RomParser
{
    public class Gen1RomParser : IRomParser
    {
        public int Generation => 1;

        private readonly ICharMap _charMap = new Gen1CharMap();

        private readonly MoveDataExtractor _moveDataExtractor = new();
        private readonly PokemonDataExtractor _pokemonDataExtractor = new();
        private readonly TypeDataExtractor _typeDataExtractor = new();

        public bool CanParse(byte[] fileData)
        {
            // Simple heuristic for Gen1 ROM
            // Check Nintendo logo and game title in the ROM header (0x134 to 0x143)
            var gameTitle = ByteReader.ReadString(fileData, 0x134, 11);
            return gameTitle.StartsWith("POKEMON", StringComparison.OrdinalIgnoreCase);
        }

        public ParserResult<ParsedRomData> ExtractRomData(byte[] data, RomOffsetsMap offsetsMap)
        {
            try
            {
                var reader = new ByteReader(data, _charMap);

                // Extract data using the provided offsets
                var moves = _moveDataExtractor.ExtractMoves(reader, offsetsMap.Moves);
                var moveNames = _moveDataExtractor.ExtractMoveNames(reader, offsetsMap.MoveNames);

                _pokemonDataExtractor.LoadPokedexOrderMap(data, offsetsMap.Pokedex);
                var pokemonStats = _pokemonDataExtractor.ExtractStats(reader, offsetsMap.PokemonStats);
                var pokemonNames = _pokemonDataExtractor.ExtractNames(reader, offsetsMap.PokemonNames);

                var types = _typeDataExtractor.ExtractTypes(reader, offsetsMap.Types);

                var parsed = new ParsedRomData
                {
                    Moves = moves,
                    MoveNames = moveNames,
                    PokemonStats = pokemonStats,
                    PokemonNames = pokemonNames,
                    Types = types
                };

                return ParserResult<ParsedRomData>.SuccessResult(parsed);
            }
            catch (Exception ex)
            {
                return ParserResult<ParsedRomData>.FailureResult([$"Failed to parse Gen1 ROM: {ex.Message}"]);
            }
        }

        public RomInfo ParseRomInfo(byte[] data)
        {
            // You can add version, checksum, etc.
            var title = ByteReader.ReadString(data, 0x134, 11);
            var versionByte = data[0x14E];
            return new RomInfo
            {
                Title = title,
                Generation = Generation,
                Version = versionByte.ToString("X2")
            };
        }
    }
}