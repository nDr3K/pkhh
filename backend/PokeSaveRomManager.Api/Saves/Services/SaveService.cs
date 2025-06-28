using PokeSaveRomManager.Api.Saves.Models;
using PokeSaveRomManager.Api.Saves.DTOs;
using PokeSaveRomManager.Api.Saves.Mapper;
using PokeSaveRomManager.Api.Saves.Repositories;
using PokeSaveRomManager.Api.Saves.Services.Handler;
using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Services;
using PokeSaveRomManager.Data.Domain;
using PokeSaveRomManager.Api.Shared.Models;
using PokeSaveRomManager.Parser.Core.Models.Data;
using BoxData = PokeSaveRomManager.Api.Saves.Models.BoxData;

namespace PokeSaveRomManager.Api.Saves.Services
{
    public class SaveService : ISaveService
    {
        private readonly ISaveRepository _repository;
        private readonly ISaveServiceHandler _saveServiceHandler;
        private readonly ILogger<SaveService> _logger;
        private readonly ParserOrchestrator _parserOrchestrator;

        public SaveService(ISaveRepository repository, ISaveServiceHandler saveServiceHandler, ILogger<SaveService> logger, ParserOrchestrator parserOrchestrator)
        {
            _repository = repository;
            _saveServiceHandler = saveServiceHandler;
            _logger = logger;
            _parserOrchestrator = parserOrchestrator;
        }

        public async Task<PagedResponse<SaveDto>> GetAllAsync(string userId, int pageNumber, int pageSize)
        {
            try
            {
                var user = await _saveServiceHandler.GetUserIdByAuthId(userId);
                var (saves, totalCount) = await _repository.GetAllAsync(user, pageNumber, pageSize);
                _logger.LogInformation("Retrieved {Count} saves for User with ID {UserId}", saves.Count(), userId);
                return new PagedResponse<SaveDto>(saves.ToDtos(), totalCount, pageNumber, pageSize);
            }
            catch
            {
                _logger.LogError("Error retrieving saves for User with ID {UserId}", userId);
                throw;
            }
        }

        public async Task<SaveDetailDto> GetByIdAsync(int id)
        {
            try
            {
                var save = await _repository.GetByIdAsync(id);
                _logger.LogInformation("Retrieved save with ID {Id}", id);
                return save.ToDetailDto();
            }
            catch
            {
                _logger.LogError("Error retrieving save with ID {Id}", id);
                throw;
            }
        }

        public async Task<SaveDetailDto> Create(string userId, SaveFileUploadDto save)
        {
            var transaction = await _repository.BeginTransactionAsync();
            try
            {
                var saveData = ProcessSaveFile(save.SaveFile);
                _logger.LogInformation("Processed save file with {Size} bytes", save.SaveFile.Length);

                var pokemonData = await _saveServiceHandler.GetDatas(saveData, save.Metadata.GameId);
                _logger.LogInformation("Parsed save file data for user {UserId}", userId);

                // Determine ID to use for Pokémon instance association
                var id = await _saveServiceHandler.GetUserIdByAuthId(userId);
                var currentSave = await _repository.Create(saveData.PlayerData.ToDomain(id, save.Metadata));
                _logger.LogInformation("Created new save for user {UserId}", userId);

                // Process Pokemon data and associate with team/boxes
                await ProcessPokemonData(pokemonData, currentSave);

                await _repository.SaveChangesAsync();
                await transaction.CommitAsync();

                var addedSave = await _repository.GetByIdAsync(currentSave.Id);
                return addedSave.ToDetailDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing save for user {UserId}", userId);
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        public async Task<SaveDetailDto> Update(string userId, int saveId, SaveFileUploadDto save)
        {
            var transaction = await _repository.BeginTransactionAsync();
            try
            {
                var existingSave = await _repository.GetByIdWithReferences(saveId);
                if (existingSave == null)
                {
                    _logger.LogError("Save with ID {Id} not found", saveId);
                    throw new Exception($"Save with ID {saveId} not found");
                }
                if (existingSave.User.Auth0Id != userId)
                {
                    _logger.LogError("User {UserId} is not authorized to update save with ID {Id}", userId, saveId);
                    throw new Exception($"User {userId} is not authorized to update this save");
                }

                // Delete existing pokemon instances and references
                if (existingSave.Party != null)
                {
                    await _saveServiceHandler.DeleteParty(existingSave.Party.Id);
                }
                var box = existingSave.Boxes.FirstOrDefault();
                if (box != null)
                {
                    await _saveServiceHandler.DeleteBox(box.Id);
                }
                await _saveServiceHandler.DeletePokemonInstances(saveId);

                var saveData = ProcessSaveFile(save.SaveFile);
                _logger.LogInformation("Processed save file with {Size} bytes", save.SaveFile.Length);

                var pokemonData = await _saveServiceHandler.GetDatas(saveData, save.Metadata.GameId);
                _logger.LogInformation("Parsed save file data for user {UserId}", userId);

                // Process Pokemon data and associate with team/boxes
                await ProcessPokemonData(pokemonData, existingSave);

                // Update Timestamp
                existingSave.UpdatedAt = DateTime.UtcNow;

                await _repository.SaveChangesAsync();
                await transaction.CommitAsync();

                var updatedSave = await _repository.GetByIdAsync(existingSave.Id);
                return updatedSave.ToDetailDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing save for user {UserId}", userId);
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        private async Task ProcessPokemonData(SaveFileData pokemonData, Save save)
        {
            // Process party Pokemon
            var partyPokemonList = pokemonData.Party.ToList();
            _logger.LogInformation("Processing {Count} party Pokemon for save ID {SaveId}", partyPokemonList.Count, save.Id);

            var partyInstances = await _saveServiceHandler.SavePokemonInstances(partyPokemonList, save.Id);
            var partyInstanceIds = partyInstances.Select(p => p.Id).ToList();

            foreach (var id in partyInstanceIds)
            {
                save.Party.Members.Add(new SaveTeamMember
                {
                    PokemonInstanceId = id,
                    PartyId = save.Party.Id,
                });
            }

            // Process box Pokemon
            var boxPokemonList = pokemonData.Boxes.ToList();
            _logger.LogInformation("Processing {Count} box Pokemon for save ID {SaveId}", boxPokemonList.Count, save.Id);

            foreach (var boxData in boxPokemonList)
            {
                var box = await EnsureBoxExists(save, boxData);
                var boxInstances = await _saveServiceHandler.SavePokemonInstances(boxData.Slots, save.Id);
                var boxInstanceIds = boxInstances.Select(p => p.Id).ToList();


                foreach (var pokemon in boxInstanceIds.Select((id, index) => new { id, index }))
                {
                    box.Slots.Add(new SaveBoxSlot
                    {
                        PokemonInstanceId = pokemon.id,
                        BoxId = box.Id,
                        SlotNumber = pokemon.index + 1,
                    });
                }
            }
        }

        private async Task<SaveBox> EnsureBoxExists(Save save, BoxData boxData)
        {
            var existingBox = save.Boxes.FirstOrDefault();
            if (existingBox == null)
            {
                var box = SaveMapper.CreateSaveBox(save.Id, boxData);
                save.Boxes.Add(box);
                return await _saveServiceHandler.CreateBox(box);
            }
            return existingBox;
        }

        private ParsedSaveData ProcessSaveFile(IFormFile saveFile)
        {
            try
            {
                // Read the save file
                using var memoryStream = new MemoryStream();
                saveFile.CopyTo(memoryStream);
                byte[] saveBytes = memoryStream.ToArray();

                // Parse the save data
                var result = _parserOrchestrator.ParseSave(saveBytes);
                _logger.LogInformation($"Successfully parsed save with {saveBytes.Length} bytes");
                if (result.Success)
                {
                    return result.Data;
                }
                else
                {
                    // Log the failure
                    _logger.LogError($"Failed to parse save: {result.Errors}");
                    throw new Exception(result.Errors[0]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing save upload");
                throw new Exception("An error occurred while processing the save file.", ex);
            }
        }

        public async Task Delete(string userId, int saveId)
        {
            try
            {
                var existingSave = await _repository.GetByIdAsync(saveId);
                if (existingSave == null)
                {
                    _logger.LogError("Save with ID {Id} not found", saveId);
                    throw new Exception($"Save with ID {saveId} not found");
                }
                if (existingSave.User.Auth0Id != userId)
                {
                    _logger.LogError("User {UserId} is not authorized to delete save with ID {Id}", userId, saveId);
                    throw new Exception($"User {userId} is not authorized to delete this save");
                }
                await _saveServiceHandler.DeletePokemonInstances(saveId);
                await _repository.Delete(saveId);
                _logger.LogInformation("Deleted save with ID {Id}", saveId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting save for user {UserId}", userId);
                throw;
            }
        }
    }
}
