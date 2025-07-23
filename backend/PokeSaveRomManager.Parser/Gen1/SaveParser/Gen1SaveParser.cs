using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Models.Data;
using PokeSaveRomManager.Parser.Core.Parsers;
using PokeSaveRomManager.Parser.Core.Utils;

namespace PokeSaveRomManager.Parser.Gen1.SaveParser
{
    public class Gen1SaveParser : ISaveParser
    {
        public int Generation => 1;

        private readonly ICharMap _charMap = new Gen1CharMap();

        private PokemonDataExtractor _pokemonDataExtractor = new PokemonDataExtractor();

        private const int SAVE_SIZE = 0x8000;  // 32KB total save size
        private const int PLAYER_NAME_OFFSET = 0x2598;
        private const int PLAYER_NAME_LENGTH = 11;

        // Party
        private const int PARTY_COUNT_OFFSET = 0x2F2C;
        private const int PARTY_SPECIES_OFFSET = 0x2F2D;
        private const int PARTY_DATA_OFFSET = 0x2F34;

        // Pokedex
        private const int OWNED_OFFSET = 0x25B6;
        private const int SEEN_OFFSET = 0x25A3;
        private const int NUM_POKEMON = 151;

        private const int BADGE_DATA_OFFSET = 0x2602;

        private const int TIME_PLAYED_HOURS_OFFSET = 0x2CED;
        private const int TIME_PLAYED_MINUTES_OFFSET = 0x2CEE;
        private const int TIME_PLAYED_SECONDS_OFFSET = 0x2CEF;

        // Pokémon box related offsets
        private const int CURRENT_BOX_NUM_OFFSET = 0x284C;
        private const int CURRENT_BOX_OFFSET = 0x30C0;
        private const int BOX_1_OFFSET = 0x4000;
        private const int BOX_6_OFFSET = 0x6000;
        private const int BOX_SIZE = 0x462;
        private const int NUM_BOXES = 12;
        private const int BOX_CAPACITY = 20;

        // Magic values for save file validation
        private const byte CHECKSUM_INIT = 0xFF;
        private const int CHECKSUM_OFFSET = 0x3523;

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
                return ParserResult<ParsedSaveData>.FailureResult(["Invalid Gen 1 save file"]);
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
                saveData.PlayerData.GameTime = ExtractGameTime(data);
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
                // Gen 1 saves use a checksum for data validation
                byte calculatedChecksum = CalculateChecksum(data);
                byte savedChecksum = data[CHECKSUM_OFFSET];

                // --- ADD THIS LOGGING ---
                Console.WriteLine($"Saved Checksum (from file at 0x3523): {savedChecksum}");
                Console.WriteLine($"Calculated Checksum (from 0x2598-0x3522): {calculatedChecksum}");

                return calculatedChecksum == savedChecksum;
            }
            catch
            {
                return false;
            }
        }

        private byte CalculateChecksum(byte[] data)
        {
            // Checksum calculation for Gen 1 games
            byte checksum = CHECKSUM_INIT;

            // Checksum is calculated from 0x2598 to 0x3522
            for (int i = 0x2598; i <= 0x3522; i++)
            {
                checksum -= data[i];
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
            byte badgeByte = data[BADGE_DATA_OFFSET];
            var badges = new List<Badge>();

            // Check each bit for badge presence
            if ((badgeByte & 0x01) != 0) badges.Add(new Badge(Gen1Badge.Boulder, 1));
            if ((badgeByte & 0x02) != 0) badges.Add(new Badge(Gen1Badge.Cascade, 2));
            if ((badgeByte & 0x04) != 0) badges.Add(new Badge(Gen1Badge.Thunder, 3));
            if ((badgeByte & 0x08) != 0) badges.Add(new Badge(Gen1Badge.Rainbow, 4));
            if ((badgeByte & 0x10) != 0) badges.Add(new Badge(Gen1Badge.Soul, 5));
            if ((badgeByte & 0x20) != 0) badges.Add(new Badge(Gen1Badge.Marsh, 6));
            if ((badgeByte & 0x40) != 0) badges.Add(new Badge(Gen1Badge.Volcano, 7));
            if ((badgeByte & 0x80) != 0) badges.Add(new Badge(Gen1Badge.Earth, 8));

            return badges;
        }

        private GameTime ExtractGameTime(byte[] data)
        {
            int hours = data[TIME_PLAYED_HOURS_OFFSET];
            int minutes = data[TIME_PLAYED_MINUTES_OFFSET];
            int seconds = data[TIME_PLAYED_SECONDS_OFFSET];

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
            // 6 bytes - species IDs
            // N bytes - individual Pokémon data

            for (int i = 0; i < partyCount; i++)
            {
                int offset = PARTY_DATA_OFFSET + (i * 44); // Each pokemon data block is 44 bytes

                // Extract individual Pokémon data with all properties
                var pokemon = _pokemonDataExtractor.ExtractPokemon(data, offset, true);
                pokemon.PokemonId = data[PARTY_SPECIES_OFFSET + i];

                party.Add(pokemon);
            }

            return party;
        }

        private List<BoxData> ExtractBoxes(byte[] data)
        {
            var boxes = new List<BoxData>();

            // Current box
            int currentBoxNum = data[CURRENT_BOX_NUM_OFFSET] & 0x7F; // Mask out the high bit

            // Extract each box
            for (int boxIdx = 0; boxIdx < NUM_BOXES; boxIdx++)
            {
                var box = new BoxData()
                {
                    BoxIndex = boxIdx,
                    MaxPokemonCount = BOX_CAPACITY,
                    Pokemon = new List<PokemonSaveData>()
                };

                int boxOffset;
                if (boxIdx == currentBoxNum)
                {
                    boxOffset = CURRENT_BOX_OFFSET; // Current box is in WRAM
                }
                else
                {
                    int bankOffset = (boxIdx < 6 ? BOX_1_OFFSET : BOX_6_OFFSET); // Boxes 1-6 are in the second bank, 7-12 in the third bank
                    int boxInBankIdx = boxIdx < 6 ? boxIdx : boxIdx - 6;
                    boxOffset = bankOffset + (boxInBankIdx * BOX_SIZE);
                }

                int boxCount = data[boxOffset];

                if (boxCount > 20) boxCount = BOX_CAPACITY; // Safety check

                box.PokemonCount = boxCount;

                for (int i = 0; i < boxCount; i++)
                {
                    int offset = boxOffset + 2 + BOX_CAPACITY + (i * 33); // Box Pokemon structure is 33 bytes per Pokemon

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