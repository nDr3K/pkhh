
using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Models.Data;
using PokeSaveRomManager.Parser.Core.Parsers;
using PokeSaveRomManager.Parser.Core.Utils;
using PokeSaveRomManager.Parser.Gen1.SaveParser;

namespace PokeSaveRomManager.Parser.Gen2.SaveParser
{
    public class Gen2SaveParser : ISaveParser
    {
        public int Generation => 2;

        private readonly ICharMap _charMap = new GBCharMap();

        private PokemonDataExtractor _pokemonDataExtractor = new PokemonDataExtractor();

        private const int SAVE_SIZE = 0x8030;  // total save size
        private const int PLAYER_NAME_OFFSET = 0x200B;
        private const int PLAYER_NAME_LENGTH = 11;

        // Party
        private const int PARTY_COUNT_OFFSET = 0x288A;
        private const int PARTY_SPECIES_OFFSET = 0x288B;
        private const int PARTY_DATA_OFFSET = 0x2892;

        // Pokedex
        private const int OWNED_OFFSET = 0x2A4C;
        private const int SEEN_OFFSET = 0x2A6C;
        private const int NUM_POKEMON = 251;

        // Badges
        private const int BADGE_JOHTO_DATA_OFFSET = 0x23E4;
        private const int BADGE_KANTO_DATA_OFFSET = 0x23E5;

        // GameTime
        private const int TIME_PLAYED_OFFSET = 0x2053;

        // Pokémon box related offsets
        private const int BOX_1_OFFSET = 0x4000;
        private const int BOX_7_OFFSET = 0x6000;
        private const int BOX_SIZE = 0x450;
        private const int NUM_BOXES = 14;
        private const int BOX_CAPACITY = 20;

        // Checksum values for save file validation
        private const int CHECKSUM_OFFSET = 0x2D69;

        public bool CanParse(byte[] fileData)
        {
            // Check file length and basic validation
            if (fileData.Length < SAVE_SIZE)
                return false;

            return ValidateSavefile(fileData);
        }

        public ParserResult<ParsedSaveData> ExtractSaveData(byte[] data)
        {
            if (!ValidateSavefile(data))
            {
                return ParserResult<ParsedSaveData>.FailureResult(["Invalid Gen 2 save file"]);
            }

            var saveData = new ParsedSaveData()
            {
                PlayerData = new PlayerData(),
                Boxes = new List<BoxData>(),
                Party = new List<PokemonSaveData>()
            };

            var reader = new ByteReader(data, _charMap);

            try
            {
                saveData.PlayerData.Name = ExtractPlayerName(reader, data);
                saveData.PlayerData.Badges = ExtractBadges(data);
                saveData.PlayerData.GameTime = ExtractGameTime(reader, data);
                saveData.PlayerData.Pokedex = ExtractPokedex(data);
                saveData.Party = ExtractParty(data);
                saveData.Boxes = ExtractBoxes(data);

                return ParserResult<ParsedSaveData>.SuccessResult(saveData);
            }
            catch (Exception ex)
            {
                return ParserResult<ParsedSaveData>.FailureResult([$"Error parsing save file: {ex.Message}"]);
            }
        }


        public bool ValidateSavefile(byte[] data)
        {
            if (data.Length < SAVE_SIZE)
                return false;

            try
            {
                // Gen 2 saves use a checksum for data validation
                byte calculatedChecksum = CalculateChecksum(data);
                byte savedChecksum = data[CHECKSUM_OFFSET];

                return calculatedChecksum == savedChecksum;
            }
            catch
            {
                return false;
            }
        }

        private byte CalculateChecksum(byte[] data)
        {
            // Checksum calculation for Gen 2 games
            byte checksum = 0;

            // Checksum is calculated from 0x2D69 to 0x2D0D
            for (int i = 0x2009; i <= 0x2D68; i++)
            {
                checksum += data[i];
            }

            return checksum;
        }

        private string ExtractPlayerName(ByteReader reader, byte[] data)
        {
            try
            {
                byte[] nameBytes = new byte[PLAYER_NAME_LENGTH];
                Array.Copy(data, PLAYER_NAME_OFFSET, nameBytes, 0, PLAYER_NAME_LENGTH);

                return reader.ReadString(nameBytes).Trim().Replace("?", "");
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Failed to extract player name: {ex.Message}");
            }
        }

        private List<Badge> ExtractBadges(byte[] data)
        {
            byte badgeJhotoByte = data[BADGE_JOHTO_DATA_OFFSET];
            byte badgeKantoByte = data[BADGE_KANTO_DATA_OFFSET];
            var badges = new List<Badge>();

            // Check each bit for badge presence
            if ((badgeJhotoByte & 0x01) != 0) badges.Add(new Badge(Gen2Badge.Zephyr, 1));
            if ((badgeJhotoByte & 0x02) != 0) badges.Add(new Badge(Gen2Badge.Hive, 2));
            if ((badgeJhotoByte & 0x04) != 0) badges.Add(new Badge(Gen2Badge.Plain, 3));
            if ((badgeJhotoByte & 0x08) != 0) badges.Add(new Badge(Gen2Badge.Fog, 4));
            if ((badgeJhotoByte & 0x20) != 0) badges.Add(new Badge(Gen2Badge.Mineral, 6));
            if ((badgeJhotoByte & 0x40) != 0) badges.Add(new Badge(Gen2Badge.Glacier, 7));
            if ((badgeJhotoByte & 0x80) != 0) badges.Add(new Badge(Gen2Badge.Rising, 8));
            if ((badgeKantoByte & 0x01) != 0) badges.Add(new Badge(Gen1Badge.Boulder, 1));
            if ((badgeKantoByte & 0x02) != 0) badges.Add(new Badge(Gen1Badge.Cascade, 2));
            if ((badgeKantoByte & 0x04) != 0) badges.Add(new Badge(Gen1Badge.Thunder, 3));
            if ((badgeKantoByte & 0x08) != 0) badges.Add(new Badge(Gen1Badge.Rainbow, 4));
            if ((badgeKantoByte & 0x10) != 0) badges.Add(new Badge(Gen1Badge.Soul, 5));
            if ((badgeKantoByte & 0x20) != 0) badges.Add(new Badge(Gen1Badge.Marsh, 6));
            if ((badgeKantoByte & 0x40) != 0) badges.Add(new Badge(Gen1Badge.Volcano, 7));
            if ((badgeKantoByte & 0x80) != 0) badges.Add(new Badge(Gen1Badge.Earth, 8));

            return badges;
        }

        private GameTime ExtractGameTime(ByteReader reader, byte[] data)
        {
            int hours = (data[TIME_PLAYED_OFFSET] * 100) + data[TIME_PLAYED_OFFSET+1];
            int minutes = data[TIME_PLAYED_OFFSET+2];
            int seconds = data[TIME_PLAYED_OFFSET+3];

            var gameTime = new GameTime
            {
                Hours = hours,
                Minutes = minutes,
                Seconds = seconds
            };
            return gameTime;
        }

        private List<PokemonSaveData> ExtractParty(byte[] data)
        {
            var party = new List<PokemonSaveData>();

            int partyCount = data[PARTY_COUNT_OFFSET];
            if (partyCount > 6) partyCount = 6; // Safety check

            // Party structure:
            // 1 byte - number of Pokémon in party
            // 49 bytes - species IDs
            // N bytes - individual Pokémon data
            try
            {
                for (int i = 0; i < partyCount; i++)
                {
                    int offset = PARTY_DATA_OFFSET + (i * (48)); // Each pokemon data block is 48 bytes

                    // Extract individual Pokémon data with all properties
                    var pokemon = _pokemonDataExtractor.ExtractPokemon(data, offset, true);
                    pokemon.PokemonId = data[PARTY_SPECIES_OFFSET + i];

                    party.Add(pokemon);
                }
            } catch (Exception ex) { Console.WriteLine(ex.Message); }

            return party;
        }

        private List<BoxData> ExtractBoxes(byte[] data)
        {
            var boxes = new List<BoxData>();


            // Extract each box
            for (int boxIdx = 0; boxIdx < NUM_BOXES; boxIdx++)
            {
                var box = new BoxData()
                {
                    BoxIndex = boxIdx,
                    MaxPokemonCount = BOX_CAPACITY,
                    Pokemon = new List<PokemonSaveData>()
                };

                int bankOffset = (boxIdx < 7 ? BOX_1_OFFSET : BOX_7_OFFSET); // Boxes 1-7 are in the second bank, 8-14 in the third bank
                int boxInBankIdx = boxIdx < 7 ? boxIdx : boxIdx - 7;
                int boxOffset = bankOffset + (boxInBankIdx * BOX_SIZE);

                int boxCount = data[boxOffset];

                if (boxCount > 20) boxCount = BOX_CAPACITY; // Safety check

                box.PokemonCount = boxCount;

                for (int i = 0; i < boxCount; i++)
                {
                    int offset = boxOffset + 2 + BOX_CAPACITY + (i * 32); // Box Pokemon structure is 32 bytes per Pokemon

                    var pokemon = _pokemonDataExtractor.ExtractPokemon(data, offset, false);
                    pokemon.PokemonId = data[boxOffset + 1 + i];

                    if (pokemon.PokemonId == 0 || pokemon.PokemonId == 255)
                        continue; // Skip empty slots
                    box.Pokemon.Add(pokemon);
                }

                boxes.Add(box);
            }

            return boxes;
        }

        private Pokedex ExtractPokedex(byte[] data)
        {

            int owned = 0;
            int seen = 0;

            for (int i = 0; i <= NUM_POKEMON; i++)
            {
                int byteIndex = i / 8;
                int bitIndex = i % 8;
                byte ownedByte = data[OWNED_OFFSET + byteIndex];
                byte seenByte = data[SEEN_OFFSET + byteIndex];

                if ((ownedByte & (1 << bitIndex)) != 0)
                    owned++;

                if ((seenByte & (1 << bitIndex)) != 0)
                    seen++;
            }

            var pokedex = new Pokedex
            {
                Seen = seen,
                Owned = owned,
                Total = NUM_POKEMON,
            };

            return pokedex;
        }
    }
}
