using PokeSaveRomManager.Parser.Core.Models.Data;

namespace PokeSaveRomManager.Parser.Gen1.SaveParser
{
    public class PokemonDataExtractor
    {
        public PokemonSaveData ExtractPokemon(byte[] data, int offset, bool isFromParty)
        {
            // Read IVs first for HPIV calculation
            byte iv1 = data[offset + 26]; // Attack/Defense IV
            byte iv2 = data[offset + 27]; // Speed/Special IV

            return new PokemonSaveData
            {
                PokemonId = data[offset],                                // Species ID
                CurrentHp = BitConverter.ToUInt16(data, offset + 1),
                Level = isFromParty ? data[offset + 0x21] : data[offset + 3],
                Status = data[offset + 4],
                Type1 = data[offset + 5],
                Type2 = data[offset + 6],
                HeldItem = data[offset + 7],                             // catch rate / held item

                Move1 = new PokemonMoveData
                {
                    MoveId = data[offset + 8],
                    PP = data[offset + 28] & 0x3F
                },
                Move2 = new PokemonMoveData
                {
                    MoveId = data[offset + 9],
                    PP = data[offset + 29] & 0x3F
                },
                Move3 = new PokemonMoveData
                {
                    MoveId = data[offset + 10],
                    PP = data[offset + 30] & 0x3F
                },
                Move4 = new PokemonMoveData
                {
                    MoveId = data[offset + 11],
                    PP = data[offset + 31] & 0x3F
                },

                Experience = (data[offset + 13] << 16) |
                             (data[offset + 14] << 8) |
                             data[offset + 15],

                HPEV = BitConverter.ToUInt16(data, offset + 16),
                AttackEV = BitConverter.ToUInt16(data, offset + 18),
                DefenseEV = BitConverter.ToUInt16(data, offset + 20),
                SpeedEV = BitConverter.ToUInt16(data, offset + 22),
                SpecialEV = BitConverter.ToUInt16(data, offset + 24),
                SpecialAttackEV = null,
                SpecialDefenseEV = null,

                AttackIV = (iv1 >> 4) & 0xF,
                DefenseIV = iv1 & 0xF,
                SpeedIV = (iv2 >> 4) & 0xF,
                SpecialIV = iv2 & 0xF,
                HPIV = CalculateHPIV(
                    (iv1 >> 4) & 0xF,
                    iv1 & 0xF,
                    (iv2 >> 4) & 0xF,
                    iv2 & 0xF
                ),
                SpecialAttackIV = null,
                SpecialDefenseIV = null,

                MaxHp = isFromParty ? BitConverter.ToUInt16(data, offset + 33) : null,
                Attack = isFromParty ? BitConverter.ToUInt16(data, offset + 35) : null,
                Defense = isFromParty ? BitConverter.ToUInt16(data, offset + 37) : null,
                Speed = isFromParty ? BitConverter.ToUInt16(data, offset + 39) : null,
                Special = isFromParty ? BitConverter.ToUInt16(data, offset + 41) : null,
                SpecialAttack = null,
                SpecialDefense = null
            };
        }

        private int CalculateHPIV(int attackIV, int defenseIV, int speedIV, int specialIV)
        {
            // In Gen 1, HP IV is determined by the least significant bit of the other IVs
            int hpIV = 0;
            hpIV |= (attackIV & 1) << 3;   // Bit 3 comes from Attack
            hpIV |= (defenseIV & 1) << 2;  // Bit 2 comes from Defense
            hpIV |= (speedIV & 1) << 1;    // Bit 1 comes from Speed
            hpIV |= (specialIV & 1);       // Bit 0 comes from Special

            return hpIV;
        }
    }
}