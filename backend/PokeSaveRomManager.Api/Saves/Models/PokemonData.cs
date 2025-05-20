namespace PokeSaveRomManager.Api.Saves.Models
{
    public class PokemonData
    {
        public int PokemonId { get; set; }
        public int FormId { get; set; }

        public int? AbilityId { get; set; }

        public int? Move1Id { get; set; }
        public int? Move2Id { get; set; }
        public int? Move3Id { get; set; }
        public int? Move4Id { get; set; }

        public int Level { get; set; }

        //EVs
        public int HPEV { get; set; }
        public int AttackEV { get; set; }
        public int DefenseEV { get; set; }
        public int SpeedEV { get; set; }
        public int? SpecialEV { get; set; }
        public int? SpecialAttackEV { get; set; }
        public int? SpecialDefenseEV { get; set; }

        //IVs
        public int HPIV { get; set; }
        public int AttackIV { get; set; }
        public int DefenseIV { get; set; }
        public int SpeedIV { get; set; }
        public int? SpecialIV { get; set; }
        public int? SpecialAttackIV { get; set; }
        public int? SpecialDefenseIV { get; set; }

        //Stats - Nullable for boxes
        public int? MaxHp { get; set; }
        public int? Attack { get; set; }
        public int? Defense { get; set; }
        public int? Speed { get; set; }
        public int? Special { get; set; }
        public int? SpecialAttack { get; set; }
        public int? SpecialDefense { get; set; }
    }
}
