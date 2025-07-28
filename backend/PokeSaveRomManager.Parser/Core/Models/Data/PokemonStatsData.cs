namespace PokeSaveRomManager.Parser.Core.Models.Data
{
    public class PokemonStatsData
    {
        public int InternalId { get; set; }
        public int? DexNumber { get; set; }
        public int BaseHP { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int? BaseSpecial { get; set; }
        public int? BaseSpAttack { get; set; }
        public int? BaseSpDefense { get; set; }
        public int BaseSpeed { get; set; }
        public int Type1Id { get; set; }
        public int Type2Id { get; set; }
        public int CatchRate { get; set; }
        public int BaseExpYield { get; set; }
        public int HeldItem { get; set; }
        public int GenderRatio { get; set; }
        public int GrowthRate { get; set; }
    }
}