using PokeSaveRomManager.Api.Roms.DTOs;
using PokeSaveRomManager.Api.Roms.Mapper;
using PokeSaveRomManager.Api.Roms.Services.Handler;
using PokeSaveRomManager.Parser.Services;

namespace PokeSaveRomManager.Api.Roms.Services
{
    public class RomService : IRomService
    {
        private readonly ILogger<RomService> _logger;
        private readonly ParserOrchestrator _parserOrchestrator;
        private readonly IRomServiceHandler _romServiceHandler;
        private readonly IRomStorageService _romStorageService;

        public RomService(ILogger<RomService> logger, ParserOrchestrator parserOrchestrator, IRomServiceHandler romServiceHandler, IRomStorageService romStorageService)
        {
            _logger = logger;
            _parserOrchestrator = parserOrchestrator;
            _romServiceHandler = romServiceHandler;
            _romStorageService = romStorageService;
        }

        public async Task UploadRomAsync(RomUploadDto romDto)
        {
            try
            {
                // Read the ROM file
                using var memoryStream = new MemoryStream();
                romDto.RomFile.CopyTo(memoryStream);
                byte[] romBytes = memoryStream.ToArray();

                // Parse the ROM data
                var result = _parserOrchestrator.ParseRom(romBytes, romDto.Metadata.Offsets);
                _logger.LogInformation($"Successfully parsed ROM '{romDto.Metadata.Name}' with {romBytes.Length} bytes");
                if (result.Success)
                {
                    // Persist the ROM file
                    var romFilePath = await _romStorageService.PersistRom(romDto);
                    _logger.LogInformation($"ROM file '{romFilePath}' persisted successfully");
                    // Update the metadata with the file path
                    var romSaveDto = romDto.Metadata.MapToRomSaveDto(romFilePath);
                    // Log the successful parsing
                    await _romServiceHandler.RegisterRomDataAsync(romSaveDto, result.Data);
                    _logger.LogInformation($"Successfully registered ROM data for '{romSaveDto.Name}'");
                }
                else
                {
                    // Log the failure
                    _logger.LogError($"Failed to parse ROM '{romDto.Metadata.Name}': {result.Errors}");
                    throw new Exception(result.Errors[0]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing ROM upload");
                throw new Exception("An error occurred while processing the ROM file.", ex);
            }
        }
    }
}
