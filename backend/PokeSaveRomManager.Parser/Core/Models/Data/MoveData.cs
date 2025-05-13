namespace PokeSaveRomManager.Parser.Core.Models.Data
{
    public class MoveData
    {
        public int Id { get; set; }
        public int Effect { get; set; }
        public int Power { get; set; }
        public int Type { get; set; }
        public int Accuracy { get; set; }
        public int PP { get; set; }
        public int Priority { get; set; }
        public Category Category { get; set; }
    }

    public enum Category
    {
        Physical = 0,
        Special = 1,
        Status = 2
    }
}