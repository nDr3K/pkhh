using PokeSaveRomManager.Api.Roms.DTOs;
using PokeSaveRomManager.Api.Roms.Services.Handler;
using PokeSaveRomManager.Parser.Services;

namespace PokeSaveRomManager.Api.Roms.Services
{
    public class RomService : IRomService
    {
        private readonly ILogger<RomService> _logger;
        private readonly ParserOrchestrator _parserOrchestrator;
        private readonly IRomServiceHandler _romServiceHandler;

        public RomService(ILogger<RomService> logger, ParserOrchestrator parserOrchestrator, IRomServiceHandler romServiceHandler)
        {
            _logger = logger;
            _parserOrchestrator = parserOrchestrator;
            _romServiceHandler = romServiceHandler;
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
                    // Log the successful parsing
                    await _romServiceHandler.RegisterRomDataAsync(romDto.Metadata, result.Data);
                    _logger.LogInformation($"Successfully registered ROM data for '{romDto.Metadata.Name}'");
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
