using PokeSaveRomManager.Api.Moves.DTOs;
using PokeSaveRomManager.Api.Moves.Mapper;
using PokeSaveRomManager.Api.Moves.Repositories;

namespace PokeSaveRomManager.Api.Moves.Services
{
    public class MoveService : IMoveService
    {
        private readonly IMoveRepository _moveRepository;
        private readonly ILogger<MoveService> _logger;

        public MoveService(IMoveRepository moveRepository, ILogger<MoveService> logger)
        {
            _moveRepository = moveRepository;
            _logger = logger;
        }

        // Move
        #region Move
        public async Task<IEnumerable<MoveDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Getting all moves");
                var moves = await _moveRepository.GetAllAsync();
                return moves.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all moves");
                throw;
            }
        }

        public async Task<MoveDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting move with id {id}");
                var move = await _moveRepository.GetByIdAsync(id);
                return move.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting move with id {id}");
                throw;
            }
        }

        public async Task<MoveDto> CreateAsync(MoveCreateDto moveCreateDto)
        {
            try
            {
                _logger.LogInformation("Creating move");
                var move = moveCreateDto.ToEntity();
                var createdMove = await _moveRepository.CreateAsync(move);
                return createdMove.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating move");
                throw;
            }
        }

        public async Task<IEnumerable<MoveDto>> AddRangeAsync(IEnumerable<MoveCreateDto> moveCreateDtos)
        {
            try
            {
                _logger.LogInformation("Adding range of moves");
                var moves = moveCreateDtos.Select(m => m.ToEntity());
                var addedMoves = await _moveRepository.AddRangeAsync(moves);
                return addedMoves.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding range of moves");
                throw;
            }
        }

        public async Task UpdateAsync(int id, MoveUpdateDto moveUpdateDto)
        {
            try
            {
                _logger.LogInformation($"Updating move with id {id}");
                var move = await _moveRepository.GetByIdAsync(id);
                if (move == null)
                {
                    _logger.LogWarning($"Move with id {id} not found");
                    return;
                }
                move.UpdateFromDto(moveUpdateDto);
                await _moveRepository.UpdateAsync(move);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating move with id {id}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting move with id {id}");
                await _moveRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting move with id {id}");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Checking if move with id {id} exists");
                return await _moveRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking if move with id {id} exists");
                throw;
            }
        }

        public async Task<IEnumerable<MoveDto>> GetByTypeIdAsync(int typeId)
        {
            try
            {
                _logger.LogInformation($"Getting moves with type id {typeId}");
                var moves = await _moveRepository.GetByTypeIdAsync(typeId);
                return moves.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting moves with type id {typeId}");
                throw;
            }
        }

        public async Task<IEnumerable<MoveDto>> GetByCategoryIdAsync(int categoryId)
        {
            try
            {
                _logger.LogInformation($"Getting moves with category id {categoryId}");
                var moves = await _moveRepository.GetByCategoryIdAsync(categoryId);
                return moves.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting moves with category id {categoryId}");
                throw;
            }
        }

        public async Task<IEnumerable<MoveDto>> GetByTypeIdAndCategoryIdAsync(int typeId, int categoryId)
        {
            try
            {
                _logger.LogInformation($"Getting moves with type id {typeId} and category id {categoryId}");
                var moves = await _moveRepository.GetByTypeIdAndCategoryIdAsync(typeId, categoryId);
                return moves.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting moves with type id {typeId} and category id {categoryId}");
                throw;
            }
        }
        #endregion

        // MoveName
        #region MoveName
        public async Task<IEnumerable<MoveNameDto>> GetAllNamesAsync()
        {
            try
            {
                _logger.LogInformation("Getting all move names");
                var moveNames = await _moveRepository.GetAllNamesAsync();
                return moveNames.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all move names");
                throw;
            }
        }

        public async Task<MoveNameDto> GetNameByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting move name with id {id}");
                var moveName = await _moveRepository.GetNameByIdAsync(id);
                return moveName?.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting move name with id {id}");
                throw;
            }
        }

        public async Task<MoveNameDto> CreateNameAsync(MoveNameCreateDto moveNameCreateDto)
        {
            try
            {
                _logger.LogInformation("Creating move name");
                var moveName = moveNameCreateDto.ToEntity();
                var createdMoveName = await _moveRepository.CreateNameAsync(moveName);
                return createdMoveName.ToDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating move name");
                throw;
            }
        }

        public async Task<IEnumerable<MoveNameDto>> AddNameRangeAsync(IEnumerable<MoveNameCreateDto> moveNameCreateDtos)
        {
            try
            {
                _logger.LogInformation("Adding range of move names");
                var moveNames = moveNameCreateDtos.Select(m => m.ToEntity());
                var createdMoveNames = await _moveRepository.AddNameRangeAsync(moveNames);
                return createdMoveNames.ToDtos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding range of move names");
                throw;
            }
        }

        public async Task UpdateNameAsync(int id, MoveNameUpdateDto moveNameUpdateDto)
        {
            try
            {
                _logger.LogInformation($"Updating move name with id {id}");
                var moveName = await _moveRepository.GetNameByIdAsync(id);
                if (moveName == null)
                {
                    _logger.LogWarning($"Move name with id {id} not found");
                    return;
                }
                moveName.UpdateFromDto(moveNameUpdateDto);
                await _moveRepository.UpdateNameAsync(moveName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating move name with id {id}");
                throw;
            }
        }

        public async Task DeleteNameAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting move name with id {id}");
                await _moveRepository.DeleteNameAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting move name with id {id}");
                throw;
            }
        }

        public async Task<bool> ExistsNameAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Checking if move name with id {id} exists");
                return await _moveRepository.ExistsNameAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking if move name with id {id} exists");
                throw;
            }
        }
        #endregion
    }
}
