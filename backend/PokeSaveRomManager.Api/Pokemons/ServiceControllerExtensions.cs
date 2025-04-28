using PokeSaveRomManager.Api.Pokemons.Repositories;
using PokeSaveRomManager.Api.Pokemons.Services;

namespace PokeSaveRomManager.Api.Pokemons
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddPokemonServices(this IServiceCollection services)
        {
            // Pokemon
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            // PokemonForm
            services.AddScoped<IPokemonFormService, PokemonFormService>();
            services.AddScoped<IPokemonFormRepository, PokemonFormRepository>();
            // PokemonAbility
            services.AddScoped<IPokemonAbilityService, PokemonAbilityService>();
            services.AddScoped<IPokemonAbilityRepository, PokemonAbilityRepository>();
            // PokemonMove
            services.AddScoped<IPokemonMoveService, PokemonMoveService>();
            services.AddScoped<IPokemonMoveRepository, PokemonMoveRepository>();

            return services;
        }
    }
}
