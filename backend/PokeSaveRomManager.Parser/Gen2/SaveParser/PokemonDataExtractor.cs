
using PokeSaveRomManager.Parser.Core.Models.Data;
using PokeSaveRomManager.Parser.Core.Utils;

namespace PokeSaveRomManager.Parser.Gen2.SaveParser
{
    public class PokemonDataExtractor
    {
        public PokemonSaveData ExtractPokemon(byte[] data, int offset, bool isFromParty)
        {
            // Read IVs first for HPIV calculation
            byte iv1 = data[offset + 0x15]; // Attack/Defense IV
            byte iv2 = data[offset + 0x16]; // Speed/Special IV

            return new PokemonSaveData
            {
                PokemonId = data[offset],
                CurrentHp = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 0x22) : null,
                Level = data[offset + 0x1F],
                Status = isFromParty ? data[offset + 0x20] : 0,
                Type1 = 0, // Gen2 Pokemon Structure has no type reference
                Type2 = 0,
                HeldItem = data[offset + 0x01],

                Move1 = new PokemonMoveData
                {
                    MoveId = data[offset + 0x02],
                    PP = data[offset + 0x17] & 0x3F,
                    MaxPP = data[offset + 0x17] & 0x3F
                },
                Move2 = new PokemonMoveData
                {
                    MoveId = data[offset + 0x03],
                    PP = data[offset + 0x18] & 0x3F,
                    MaxPP = data[offset + 0x18] & 0x3F
                },
                Move3 = new PokemonMoveData
                {
                    MoveId = data[offset + 0x04],
                    PP = data[offset + 0x19] & 0x3F,
                    MaxPP = data[offset + 0x19] & 0x3F
                },
                Move4 = new PokemonMoveData
                {
                    MoveId = data[offset + 0x05],
                    PP = data[offset + 0x1A] & 0x3F,
                    MaxPP = data[offset + 0x1A] & 0x3F
                },

                Experience = (int)ByteReader.ReadUInt24BE(data, offset + 0x08),

                HPEV = ByteReader.ReadUInt16BE(data, offset + 0x0B),
                AttackEV = ByteReader.ReadUInt16BE(data, offset + 0x0D),
                DefenseEV = ByteReader.ReadUInt16BE(data, offset + 0x0F),
                SpeedEV = ByteReader.ReadUInt16BE(data, offset + 0x11),
                SpecialEV = ByteReader.ReadUInt16BE(data, offset + 0x13),
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

                MaxHp = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 0x24) : null,
                Attack = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 0x26) : null,
                Defense = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 0x28) : null,
                Speed = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 0x2A) : null,
                Special = null,
                SpecialAttack = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 0x2C) : null,
                SpecialDefense = isFromParty ? ByteReader.ReadUInt16BE(data, offset + 0x2E) : null,
            };
        }

        private int CalculateHPIV(int attackIV, int defenseIV, int speedIV, int specialIV)
        {
            // In Gen 2, HP IV is determined by the least significant bit of the other IVs
            int hpIV = 0;
            hpIV |= (attackIV & 1) << 3;   // Bit 3 comes from Attack
            hpIV |= (defenseIV & 1) << 2;  // Bit 2 comes from Defense
            hpIV |= (speedIV & 1) << 1;    // Bit 1 comes from Speed
            hpIV |= (specialIV & 1);       // Bit 0 comes from Special

            return hpIV;
        }
    }
}
