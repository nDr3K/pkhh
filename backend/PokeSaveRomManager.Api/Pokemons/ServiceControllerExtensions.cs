using PokeSaveRomManager.Api.Pokemons.Repositories;
using PokeSaveRomManager.Api.Pokemons.Services;

namespace PokeSaveRomManager.Api.Pokemons
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddPokemonServices(this IServiceCollection services)
        {
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();

            return services;
        }
    }
}
