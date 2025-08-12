namespace PokeSaveRomManager.Api.Roms.Models
{
    public class PokemonData : PartialPokemonData
    {
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
        public int GrowthRate { get; set; }
    }

    public class PartialPokemonData
    {
        public int InternalId { get; set; }
        public string Name { get; set; }
        public int? DexNumber { get; set; }
    }
}
