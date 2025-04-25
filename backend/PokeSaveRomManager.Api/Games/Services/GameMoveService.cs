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

        public async Task<IEnumerable<GameMoveDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all game moves");
                var gameMoves = await _gameMoveRepository.GetAllAsync();
                return gameMoves.ToDtos(); // GameMoveMapper Collection Method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all game moves");
                throw;
            }
        }

        public async Task<GameMoveDto> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving game move with ID: {Id}", id);
                var gameMove = await _gameMoveRepository.GetByIdAsync(id);
                return GameMapper.ToDto(gameMove);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving game move with ID: {Id}", id);
                throw;
            }
        }

        public async Task<GameMoveDto> AddAsync(GameMoveCreateDto gameMoveDto)
        {
            try
            {
                _logger.LogInformation("Adding new game move {GameMoveMoveId} for {GameMoveGameId}", gameMoveDto.MoveId, gameMoveDto.GameId);
                var gameMove = GameMapper.ToEntity(gameMoveDto);
                var addedGameMove = await _gameMoveRepository.AddAsync(gameMove);
                return GameMapper.ToDto(addedGameMove);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding game move {GameMoveMoveId} for {GameMoveGameId}", gameMoveDto.MoveId, gameMoveDto.GameId);
                throw;
            }
        }

        public async Task UpdateAsync(int id, GameMoveUpdateDto gameMoveDto)
        {
            try
            {
                _logger.LogInformation("Updating game move {GameMoveMoveId} for {GameMoveGameId}", gameMoveDto.MoveId, gameMoveDto.GameId);
                var gameMove = await _gameMoveRepository.GetByIdAsync(id);
                gameMove.UpdateFromDto(gameMoveDto);
                await _gameMoveRepository.UpdateAsync(gameMove);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating game move {GameMoveMoveId} for {GameMoveGameId}", gameMoveDto.MoveId, gameMoveDto.GameId);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting game move with ID: {Id}", id);
                await _gameMoveRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting game move with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                _logger.LogInformation("Checking existence of game move with ID: {Id}", id);
                return await _gameMoveRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking existence of game move with ID: {Id}", id);
                throw;
            }
        }
    }
}
