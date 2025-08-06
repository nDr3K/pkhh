namespace PokeSaveRomManager.Parser.Core.Models.Data
{
    public class PokemonSaveData
    {
        public int PokemonId { get; set; }
        public int? CurrentHp { get; set; }
        public int Level { get; set; }
        public int Status { get; set; }
        public int Type1 { get; set; }
        public int Type2 { get; set; }
        public int HeldItem { get; set; }
        public int Experience { get; set; }
        public PokemonMoveData Move1 { get; set; }
        public PokemonMoveData Move2 { get; set; }
        public PokemonMoveData Move3 { get; set; }
        public PokemonMoveData Move4 { get; set; }

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

    public class PokemonMoveData
    {
        public int MoveId { get; set; }
        public int PP { get; set; }
        public int MaxPP { get; set; }
    }
}