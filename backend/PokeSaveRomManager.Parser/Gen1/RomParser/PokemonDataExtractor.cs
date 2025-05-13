using PokeSaveRomManager.Parser.Core.Models.Data;
using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Utils;

namespace PokeSaveRomManager.Parser.Gen1.RomParser
{
    public class PokemonDataExtractor
    {
        private PokedexOrderMap _pokedexOrderMap = new();

        public void LoadPokedexOrderMap(byte[] romData, DataSection pokedexOffsets)
        {
            _pokedexOrderMap.Load(romData, pokedexOffsets);
        }

        public List<PokemonStatsData> ExtractStats(ByteReader reader, DataSection section)
        {
            var list = new List<PokemonStatsData>();

            for (int i = 0; i < section.Count; i++)
            {
                int offset = section.Offset + i * section.EntryLength;

                var dexNumber = reader.ReadByte(offset + 0);
                var data = new PokemonStatsData
                {
                    InternalId = _pokedexOrderMap.GetInternalId(dexNumber),
                    DexNumber = dexNumber,
                    BaseHP = reader.ReadByte(offset + 1),
                    BaseAttack = reader.ReadByte(offset + 2),
                    BaseDefense = reader.ReadByte(offset + 3),
                    BaseSpeed = reader.ReadByte(offset + 4),
                    BaseSpecial = reader.ReadByte(offset + 5),
                    Type1Id = reader.ReadByte(offset + 6),
                    Type2Id = reader.ReadByte(offset + 7),
                    CatchRate = reader.ReadByte(offset + 8),
                    BaseExpYield = reader.ReadByte(offset + 9),
                    GrowthRate = reader.ReadByte(offset + 19),
                };

                list.Add(data);
            }

            return list;
        }

        public List<PokemonNameData> ExtractNames(ByteReader reader, DataSection section)
        {
            var list = new List<PokemonNameData>();

            for (int i = 0; i < section.Count; i++)
            {
                int offset = section.Offset + i * section.EntryLength;

                string name = reader.ReadString(offset, section.EntryLength);

                var dexNumber = _pokedexOrderMap.GetPokedexNumber(i);
                if (dexNumber == 0)
                    continue;

                list.Add(new PokemonNameData
                {
                    InternalId = i,
                    DexNumber = dexNumber,
                    Name = name
                });
            }

            return list;
        }
    }

}