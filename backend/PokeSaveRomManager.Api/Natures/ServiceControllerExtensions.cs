using PokeSaveRomManager.Api.Natures.Repositories;
using PokeSaveRomManager.Api.Natures.Services;

namespace PokeSaveRomManager.Api.Natures
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddNatureServices(this IServiceCollection services)
        {
            services.AddScoped<INatureService, NatureService>();
            services.AddScoped<INatureRepository, NatureRepository>();

            return services;
        }
    }
}
