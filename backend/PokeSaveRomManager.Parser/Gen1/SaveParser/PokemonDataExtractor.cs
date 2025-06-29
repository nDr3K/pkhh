using PokeSaveRomManager.Parser.Core.Models.Data;
using PokeSaveRomManager.Parser.Core.Utils;

namespace PokeSaveRomManager.Parser.Gen1.SaveParser
{
    public class PokemonDataExtractor
    {
        public PokemonSaveData ExtractPokemon(byte[] data, int offset, bool isFromParty)
        {
            // Read IVs first for HPIV calculation
            byte iv1 = data[offset + 27]; // Attack/Defense IV
            byte iv2 = data[offset + 28]; // Speed/Special IV

            return new PokemonSaveData
            {
                PokemonId = data[offset],                                // Species ID
                CurrentHp = ByteReader.ReadUInt16BE(data, offset + 1),
                Level = isFromParty ? data[offset + 0x21] : data[offset + 3],
                Status = data[offset + 4],
                Type1 = data[offset + 5],
                Type2 = data[offset + 6],
                HeldItem = data[offset + 7],                             // catch rate / held item

                Move1 = new PokemonMoveData
                {
                    MoveId = data[offset + 8],
                    PP = data[offset + 29] & 0x3F
                },
                Move2 = new PokemonMoveData
                {
                    MoveId = data[offset + 9],
                    PP = data[offset + 30] & 0x3F
                },
                Move3 = new PokemonMoveData
                {
                    MoveId = data[offset + 10],
                    PP = data[offset + 31] & 0x3F
                },
                Move4 = new PokemonMoveData
                {
                    MoveId = data[offset + 11],
                    PP = data[offset + 32] & 0x3F
                },

                Experience = (int)ByteReader.ReadUInt24BE(data, offset + 14),

                HPEV = ByteReader.ReadUInt16BE(data, offset + 17),
                AttackEV = ByteReader.ReadUInt16BE(data, offset + 19),
                DefenseEV = ByteReader.ReadUInt16BE(data, offset + 21),
                SpeedEV = ByteReader.ReadUInt16BE(data, offset + 23),
                SpecialEV = ByteReader.ReadUInt16BE(data, offset + 25),
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

                MaxHp = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 34) : null,
                Attack = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 36) : null,
                Defense = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 38) : null,
                Speed = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 40) : null,
                Special = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 42) : null,
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