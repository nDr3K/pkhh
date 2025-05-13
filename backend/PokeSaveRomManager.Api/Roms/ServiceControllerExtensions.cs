using PokeSaveRomManager.Api.Roms.Repositories;
using PokeSaveRomManager.Api.Roms.Services;
using PokeSaveRomManager.Api.Roms.Services.Handler;
using PokeSaveRomManager.Parser.Core.Parsers;
using PokeSaveRomManager.Parser.Registry;
using PokeSaveRomManager.Parser.Services;

namespace PokeSaveRomManager.Api.Roms
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddRomsServices(this IServiceCollection services)
        {
            // Rom
            services.AddScoped<IRomServiceHandler, RomServiceHandler>();
            services.AddScoped<IRomService, RomService>();
            services.AddScoped<IRomRepository, RomRepository>();

            // Register the Rom Parsers
            services.AddScoped<ParserOrchestrator>();
            services.AddScoped<ParserFactory>();
            services.AddScoped<IParserRegistry, ParserRegistry>();

            return services;
        }
    }
}
