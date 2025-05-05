using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Games.Mapper;
using PokeSaveRomManager.Api.Games.Repositories;

namespace PokeSaveRomManager.Api.Games.Services
{
    public class GameMoveService : IGameMoveService
    {
        private readonly IGameMoveRepository _gameMoveRepository;
        private readonly ILogger<GameMoveService> _logger;

        public GameMoveService(IGameMoveRepository gameMoveRepository, ILogger<GameMoveService> logger)
        {
            _gameMoveRepository = gameMoveRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<GameMoveDto>> GetAllAsync(int gameId)
        {
            try
            {
                _logger.LogInformation("Retrieving all moves for game with ID: {gameId}", gameId);
                var gameMoves = await _gameMoveRepository.GetAllAsync(gameId);
                return gameMoves.ToDtos(); // GameMoveMapper Collection Method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all moves for game with ID: {gameId}", gameId);
                throw;
            }
        }

        public async Task<GameMoveDto> GetByIdAsync(int gameId, int id)
        {
            try
            {
                _logger.LogInformation("Retrieving move with ID: {Id} for game with ID: {gameId}", id, gameId);
                var gameMove = await _gameMoveRepository.GetByIdAsync(gameId, id);
                return GameMapper.ToDto(gameMove);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving move with ID: {Id} for game with ID: {gameId}", id, gameId);
                throw;
            }
        }

        public async Task<GameMoveDto> AddAsync(int gameId, GameMoveCreateDto gameMoveDto)
        {
            try
            {
                _logger.LogInformation("Adding new move with ID: {MoveId} for game with ID: {GameId}", gameMoveDto.MoveId, gameId);
                var gameMove = GameMapper.ToEntity(gameId, gameMoveDto);
                var addedGameMove = await _gameMoveRepository.AddAsync(gameMove);
                return GameMapper.ToDto(addedGameMove);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding new move with ID: {MoveId} for game with ID: {GameId}", gameMoveDto.MoveId, gameId);
                throw;
            }
        }

        public async Task AddRangeAsync(int gameId, IEnumerable<GameMoveCreateDto> gameMoveDtos)
        {
            try
            {
                _logger.LogInformation("Adding {count} moves for game with ID: {gameId}", gameMoveDtos.Count(), gameId);
                var gameMoves = GameMapper.ToEntities(gameId, gameMoveDtos);
                await _gameMoveRepository.AddRangeAsync(gameMoves);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding range of moves for game with ID: {gameId}", gameId);
                throw;
            }
        }

        public async Task UpdateAsync(int gameId, int id, GameMoveUpdateDto gameMoveDto)
        {
            try
            {
                _logger.LogInformation("Updating game move with ID: {Id} for game with ID: {gameId}", id, gameId);
                var gameMove = await _gameMoveRepository.GetByIdAsync(gameId, id);
                gameMove.UpdateFromDto(gameId, gameMoveDto);
                await _gameMoveRepository.UpdateAsync(gameMove);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating game move with ID: {Id} for game with ID: {gameId}", id, gameId);
                throw;
            }
        }

        public async Task DeleteAsync(int gameId, int id)
        {
            try
            {
                _logger.LogInformation("Deleting game move with ID: {Id} for game with ID: {gameId}", id, gameId);
                await _gameMoveRepository.DeleteAsync(gameId, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting game move with ID: {Id} for game with ID: {gameId}", id, gameId);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int gameId, int id)
        {
            try
            {
                _logger.LogInformation("Checking existence of game move with ID: {Id} for game with ID: {gameId}", id, gameId);
                return await _gameMoveRepository.ExistsAsync(gameId, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of game move with ID: {Id} for game with ID: {gameId}", id, gameId);
                throw;
            }
        }
    }
}
