using PokeSaveRomManager.Api.Games.Repositories;
using PokeSaveRomManager.Api.Games.Services;

namespace PokeSaveRomManager.Api.Games
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddgGameServices(this IServiceCollection services)
        {
            // Game
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();
            // GameType
            services.AddScoped<IGameTypeRepository, GameTypeRepository>();
            services.AddScoped<IGameTypeService, GameTypeService>();
            // GameAbility
            services.AddScoped<IGameAbilityRepository, GameAbilityRepository>();
            services.AddScoped<IGameAbilityService, GameAbilityService>();
            // GameMove
            services.AddScoped<IGameMoveRepository, GameMoveRepository>();
            services.AddScoped<IGameMoveService, GameMoveService>();

            return services;
        }
    }
}
