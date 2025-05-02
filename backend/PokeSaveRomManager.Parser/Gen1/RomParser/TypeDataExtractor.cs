using PokeSaveRomManager.Parser.Core.Models.Data;
using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Utils;

namespace PokeSaveRomManager.Parser.Gen1.RomParser
{
    public class TypeDataExtractor
    {

        public List<TypeData> ExtractTypes(ByteReader reader, DataSection section)
        {
            var list = new List<TypeData>();

            for (int i = 0; i < section.Count; i++)
            {
                int offset = section.Offset + (i * section.EntryLength);
                int nameAddress = reader.ReadUInt16LE(offset);
                int fullAddress = 0x20000 + nameAddress; // Types are stored in the ROM bank 9 (0x20)

                string typeName = reader.ReadString(fullAddress);

                list.Add(new TypeData
                {
                    Id = i + 1,
                    Name = typeName
                });
            }

            return list;
        }
    }

}