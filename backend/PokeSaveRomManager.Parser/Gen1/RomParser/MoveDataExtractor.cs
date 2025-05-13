using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Models.Data;
using PokeSaveRomManager.Parser.Core.Utils;

namespace PokeSaveRomManager.Parser.Gen1.RomParser
{
    public class MoveDataExtractor
    {
        public List<MoveData> ExtractMoves(ByteReader reader, DataSection offsets)
        {
            var moves = new List<MoveData>();

            for (int i = 0; i < offsets.Count; i++)
            {
                int offset = offsets.Offset + (i * offsets.EntryLength);
                byte id = reader.ReadByte(offset);
                byte effect = reader.ReadByte(offset + 1);
                byte power = reader.ReadByte(offset + 2);
                byte type = reader.ReadByte(offset + 3);
                byte accuracyRaw = reader.ReadByte(offset + 4);
                byte pp = reader.ReadByte(offset + 5);

                var move = new MoveData
                {
                    Id = id,
                    Effect = effect,
                    Power = power,
                    Type = type,
                    Accuracy = (byte)Math.Min(100, Math.Round(accuracyRaw * 100.0 / 255)), // Normalize to 0-100
                    PP = pp,
                    Priority = 0,
                    Category = power == 0 ? Category.Status : (type <= 8 ? Category.Physical : Category.Special)
                };

                moves.Add(move);
            }

            return moves;
        }

        public List<MoveName> ExtractMoveNames(ByteReader reader, DataSection offsets)
        {
            var moveNames = new List<MoveName>();

            int currentOffset = offsets.Offset;

            for (int i = 0; i < offsets.Count; i++)
            {
                byte id = (byte)(i + 1);

                string name = reader.ReadString(currentOffset);

                var moveName = new MoveName
                {
                    Id = id,
                    Name = name
                };

                moveNames.Add(moveName);

                currentOffset += name.Length + 1; // Move to the next name offset
            }

            return moveNames;
        }
    }
}