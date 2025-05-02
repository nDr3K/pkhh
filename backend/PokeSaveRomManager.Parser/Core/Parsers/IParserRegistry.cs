namespace PokeSaveRomManager.Parser.Core.Parsers
{
    public interface IParserRegistry
    {
        void RegisterRomParser(string gameVersion, IRomParser parser);
        void RegisterSaveParser(string gameVersion, ISaveParser parser);

        IEnumerable<IRomParser> GetRomParsers();
        IEnumerable<ISaveParser> GetSaveParsers();

        IRomParser GetRomParser(string gameVersion);
        ISaveParser GetSaveParser(string gameVersion);

        bool HasRomParser(string gameVersion);
        bool HasSaveParser(string gameVersion);
    }
}