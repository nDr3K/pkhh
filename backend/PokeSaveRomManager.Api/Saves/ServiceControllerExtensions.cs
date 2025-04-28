using PokeSaveRomManager.Api.Saves.Repositories;
using PokeSaveRomManager.Api.Saves.Services;

namespace PokeSaveRomManager.Api.Saves
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddSaveServices(this IServiceCollection services)
        {
            // Save
            services.AddScoped<ISaveService, SaveService>();
            services.AddScoped<ISaveRepository, SaveRepository>();

            return services;
        }
    }
}
