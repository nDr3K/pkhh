namespace PokeSaveRomManager.Parser.Core.Parsers
{
    public interface IGameParser
    {
        int Generation { get; }
        bool CanParse(byte[] fileData);
    }
}
