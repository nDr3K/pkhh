using PokeSaveRomManager.Api.Games.Repositories;
using PokeSaveRomManager.Api.Games.Services;

namespace PokeSaveRomManager.Api.Games
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddgGameServices(this IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();

            return services;
        }
    }
}
