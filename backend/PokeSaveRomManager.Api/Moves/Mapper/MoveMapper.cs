using PokeSaveRomManager.Api.Moves.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Moves.Mapper
{
    public static class MoveMapper
    {
        // Move
        public static MoveDto ToDto(this Move move)
        {
            return new MoveDto
            {
                Id = move.Id,
                NameId = move.NameId,
                Name = move.Name?.Name,
                TypeId = move.TypeId,
                Type = move.Type?.Name,
                Category = move.Category?.Name,
                Power = move.Power,
                Accuracy = move.Accuracy,
                PP = move.PP,
                Effect = move.Effect,
                Priority = move.Priority
            };
        }
        public static IEnumerable<MoveDto> ToDtos(this IEnumerable<Move> moves)
        {
            return moves.Select(c => c.ToDto());
        }

        public static Move ToEntity(this MoveCreateDto moveCreateDto)
        {
            if (moveCreateDto == null)
                return null;

            return new Move
            {
                NameId = moveCreateDto.NameId,
                TypeId = moveCreateDto.TypeId,
                CategoryId = moveCreateDto.CategoryId,
                Power = moveCreateDto.Power,
                Accuracy = moveCreateDto.Accuracy,
                PP = moveCreateDto.PP,
                Effect = moveCreateDto.Effect,
                Priority = moveCreateDto.Priority
            };
        }

        public static void UpdateFromDto(this Move move, MoveUpdateDto moveUpdateDto)
        {
            if (move == null || moveUpdateDto == null)
                return;

            move.NameId = moveUpdateDto.NameId;
            move.TypeId = moveUpdateDto.TypeId;
            move.CategoryId = moveUpdateDto.CategoryId;
            move.Power = moveUpdateDto.Power;
            move.Accuracy = moveUpdateDto.Accuracy;
            move.PP = moveUpdateDto.PP;
            move.Effect = moveUpdateDto.Effect;
            move.Priority = moveUpdateDto.Priority;
        }

        // MoveName
        public static MoveNameDto ToDto(this MoveName moveName)
        {
            return new MoveNameDto
            {
                Id = moveName.Id,
                Name = moveName.Name
            };
        }

        public static IEnumerable<MoveNameDto> ToDtos(this IEnumerable<MoveName> moveNames)
        {
            return moveNames.Select(c => c.ToDto());
        }

        public static MoveName ToEntity(this MoveNameCreateDto moveNameDto)
        {
            if (moveNameDto == null)
                return null;

            return new MoveName
            {
                Name = moveNameDto.Name
            };
        }

        public static void UpdateFromDto(this MoveName moveName, MoveNameUpdateDto moveNameDto)
        {
            if (moveName == null || moveNameDto == null)
                return;

            moveName.Name = moveNameDto.Name;
        }

        // MoveLearningMethod
        public static MoveLearningMethodDto ToDto(this MoveLearningMethod moveLearningMethod)
        {
            return new MoveLearningMethodDto
            {
                Id = moveLearningMethod.Id,
                Name = moveLearningMethod.Name
            };
        }

        public static IEnumerable<MoveLearningMethodDto> ToDtos(this IEnumerable<MoveLearningMethod> moveLearningMethods)
        {
            return moveLearningMethods.Select(c => c.ToDto());
        }

        public static MoveLearningMethod ToEntity(this MoveLearningMethodCreateDto moveLearningMethodDto)
        {
            if (moveLearningMethodDto == null)
                return null;

            return new MoveLearningMethod
            {
                Name = moveLearningMethodDto.Name
            };
        }

        public static void UpdateFromDto(this MoveLearningMethod moveLearningMethod, MoveLearningMethodUpdateDto moveLearningMethodDto)
        {
            if (moveLearningMethod == null || moveLearningMethodDto == null)
                return;

            moveLearningMethod.Name = moveLearningMethodDto.Name;
        }
    }
}
