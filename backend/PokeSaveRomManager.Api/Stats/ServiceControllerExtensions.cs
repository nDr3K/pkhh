using PokeSaveRomManager.Api.Stats.Repositories;
using PokeSaveRomManager.Api.Stats.Services;

namespace PokeSaveRomManager.Api.Stats
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddStatServices(this IServiceCollection services)
        {
            services.AddScoped<IStatService, StatService>();
            services.AddScoped<IStatRepository, StatRepository>();

            return services;
        }
    }
}
