using PokeSaveRomManager.Api.Abilities.Repositories;
using PokeSaveRomManager.Api.Abilities.Services;

namespace PokeSaveRomManager.Api.Abilities
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddAbilityServices(this IServiceCollection services)
        {
            services.AddScoped<IAbilityService, AbilityService>();
            services.AddScoped<IAbilityRepository, AbilityRepository>();

            return services;

        }
    }
}
