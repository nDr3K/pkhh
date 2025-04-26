using PokeSaveRomManager.Api.Moves.Repositories;
using PokeSaveRomManager.Api.Moves.Services;

namespace PokeSaveRomManager.Api.Moves
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddMoveServices(this IServiceCollection services)
        {
            services.AddScoped<IMoveService, MoveService>();
            services.AddScoped<IMoveRepository, MoveRepository>();

            return services;
        }
    }
}
