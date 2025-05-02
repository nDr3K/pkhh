using PokeSaveRomManager.Parser.Core.Exceptions;
using PokeSaveRomManager.Parser.Core.Parsers;
using System.Reflection.PortableExecutable;
using System.Text;

namespace PokeSaveRomManager.Parser.Core.Utils
{
    public class ByteReader
    {
        private readonly byte[] _data;
        private readonly ICharMap _charMap;
        public byte Terminator => _charMap.Terminator;

        public ByteReader(byte[] data, ICharMap charMap)
        {
            _data = data;
            _charMap = charMap;
        }

        public byte ReadByte(int offset) => _data[offset];

        public ushort ReadUInt16LE(int offset) =>
            (ushort)(_data[offset] | (_data[offset + 1] << 8));

        public ushort ReadUInt16BE(int offset) =>
            (ushort)((_data[offset] << 8) | _data[offset + 1]);

        public uint ReadUInt32LE(int offset) =>
            (uint)(_data[offset] | (_data[offset + 1] << 8) |
                   (_data[offset + 2] << 16) | (_data[offset + 3] << 24));

        public byte[] Slice(int offset, int length)
        {
            if (offset + length > _data.Length)
                throw new DataOutOfBoundsException($"Offset {offset}, Length {length}");
            return _data.Skip(offset).Take(length).ToArray();
        }

        public string ReadString(int offset, int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than 0");

            return _charMap.DecodeString(_data, offset, length);
        }

        public static string ReadString(byte[] data, int offset, int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than 0");

            var charMap = new Gen1CharMap();
            return charMap.DecodeString(data, offset, length);
        }

        public string ReadString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes)
            {
               sb.Append(_charMap.DecodeChar(b));
            }

            return sb.ToString().Trim();
        }

        public string ReadString(int offset)
        {
            List<byte> nameBytes = [];
            byte currentByte;
            int currentOffset = offset;

            while ((currentByte = ReadByte(currentOffset++)) != Terminator)
            {
                nameBytes.Add(currentByte);
            }

            return ReadString([.. nameBytes]);
        }
    }

}