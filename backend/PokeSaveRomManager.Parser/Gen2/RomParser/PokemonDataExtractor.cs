
using PokeSaveRomManager.Parser.Core.Models.Data;
using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Utils;

namespace PokeSaveRomManager.Parser.Gen2.RomParser
{
    public class PokemonDataExtractor
    {

        public List<PokemonStatsData> ExtractStats(ByteReader reader, DataSection section)
        {
            var list = new List<PokemonStatsData>();

            for (int i = 0; i < section.Count; i++)
            {
                int offset = section.Offset + i * section.EntryLength;

                var dexNumber = reader.ReadByte(offset + 0);
                var data = new PokemonStatsData
                {
                    InternalId = dexNumber,
                    DexNumber = dexNumber,
                    BaseHP = reader.ReadByte(offset + 1),
                    BaseAttack = reader.ReadByte(offset + 2),
                    BaseDefense = reader.ReadByte(offset + 3),
                    BaseSpeed = reader.ReadByte(offset + 4),
                    BaseSpAttack = reader.ReadByte(offset + 5),
                    BaseSpDefense = reader.ReadByte(offset + 6),
                    Type1Id = reader.ReadByte(offset + 7),
                    Type2Id = reader.ReadByte(offset + 8),
                    CatchRate = reader.ReadByte(offset + 9),
                    BaseExpYield = reader.ReadByte(offset + 10),
                    HeldItem = reader.ReadByte(offset + 11),
                    GenderRatio = reader.ReadByte(offset + 12),
                    GrowthRate = reader.ReadByte(offset + 19),
                };

                list.Add(data);
            }

            return list;
        }

        public List<PokemonNameData> ExtractNames(ByteReader reader, DataSection section)
        {
            var list = new List<PokemonNameData>();

            int currentOffset = section.Offset;

            for (int i = 0; i < section.Count; i++)
            {
                string name = "";
                // More then 1 terminator is used...
                while(name == "")
                {
                    name = reader.ReadString(currentOffset, 10); //Max name length is still 10 but in gen2 they are separated without a fixed memory space
                    if (name == "")
                    {
                        // Move offsets if a invalid terminator was found
                        currentOffset += 1;
                    }
                }

                // Ignore weird cases
                if (!name.Contains('?'))
                {
                    list.Add(new PokemonNameData
                    {
                        InternalId = i + 1,
                        DexNumber = i + 1,
                        Name = name
                    });
                }

                currentOffset += name.Length; // Move to the next name offset

                // Move the offsets to the next character if the limit was not reached
                if (name.Length != 10)
                {
                    currentOffset++;
                }
            }

            return list;
        }
    }

}
