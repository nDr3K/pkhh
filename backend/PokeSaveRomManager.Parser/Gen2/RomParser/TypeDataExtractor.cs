
using PokeSaveRomManager.Parser.Core.Models.Data;
using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Utils;
using System.Reflection.Metadata;

namespace PokeSaveRomManager.Parser.Gen2.RomParser
{
    public class TypeDataExtractor
    {

        public List<TypeData> ExtractTypes(ByteReader reader, DataSection section)
        {
            var list = new List<TypeData>();

            int currentOffset = section.Offset;

            for (int i = 0; i < section.Count; i++)
            {
                int offset = section.Offset + (i * section.EntryLength);
                int nameAddress = reader.ReadUInt16LE(offset);
                int fullAddress = 0x4C000 + nameAddress; // Idk ask the pokemon company why its shifted of 0x4000

                string typeName = reader.ReadString(fullAddress);

                list.Add(new TypeData
                {
                    Id = i,
                    Name = typeName
                });
            }

            return list;
        }
    }

}
