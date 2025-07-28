
using PokeSaveRomManager.Parser.Core.Parsers;
using System.Text;

namespace PokeSaveRomManager.Parser.Core.Utils
{
    public abstract class BaseCharMap : ICharMap
    {
        public abstract byte Terminator { get; }
        public abstract Dictionary<byte, string> Map { get; }

        public string DecodeString(byte[] data, int offset, int maxLength)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < maxLength && offset + i < data.Length; i++)
            {
                byte b = data[offset + i];
                Console.WriteLine(b);
                if (b == Terminator)
                    break;

                if (Map.TryGetValue(b, out string value))
                    sb.Append(value);
                else
                    sb.Append('?'); // fallback for unknown
            }
            return sb.ToString();
        }

        public string DecodeChar(byte b)
        {
            if (Map.TryGetValue(b, out string value))
                return value;
            return "?"; // fallback for unknown
        }
    }
}
