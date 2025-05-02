namespace PokeSaveRomManager.Parser.Core.Parsers
{
    public interface ICharMap
    {
        byte Terminator { get; }
        public Dictionary<byte, string> Map { get; }
        string DecodeString(byte[] data, int offset, int maxLength);
        string DecodeChar(byte b);
    }
}