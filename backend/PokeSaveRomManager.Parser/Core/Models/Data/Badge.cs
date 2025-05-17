namespace PokeSaveRomManager.Parser.Core.Models.Data
{
    public class Badge
    {
        public Badge(string name, int index)
        {
            Name = name;
            Index = index;
        }
        public string Name { get; }
        public int Index { get; }
    }
}