using PokeSaveRomManager.Api.Saves.Repositories;
using PokeSaveRomManager.Api.Saves.Services;
using PokeSaveRomManager.Api.Saves.Services.Handler;
using PokeSaveRomManager.Parser.Core.Parsers;
using PokeSaveRomManager.Parser.Registry;
using PokeSaveRomManager.Parser.Services;

namespace PokeSaveRomManager.Api.Saves
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddSaveServices(this IServiceCollection services)
        {
            // Save
            services.AddScoped<ISaveService, SaveService>();
            services.AddScoped<ISaveRepository, SaveRepository>();
            services.AddScoped<ISaveServiceHandler, SaveServiceHandler>();

            // PokemonInstance
            services.AddScoped<IPokemonInstanceRepository, PokemonInstanceRepository>();

            // Party
            services.AddScoped<IPartyRepository, PartyRepository>();

            // Box
            services.AddScoped<IBoxRepository, BoxRepository>();

            // Register the Rom Parsers
            services.AddScoped<ParserOrchestrator>();
            services.AddScoped<ParserFactory>();
            services.AddScoped<IParserRegistry, ParserRegistry>();

            return services;
        }
    }
}
