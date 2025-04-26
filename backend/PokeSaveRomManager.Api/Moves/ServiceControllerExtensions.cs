using PokeSaveRomManager.Api.Moves.Repositories;
using PokeSaveRomManager.Api.Moves.Services;

namespace PokeSaveRomManager.Api.Moves
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddMoveServices(this IServiceCollection services)
        {
            // Move
            services.AddScoped<IMoveService, MoveService>();
            services.AddScoped<IMoveRepository, MoveRepository>();
            // Move Learning Method
            services.AddScoped<IMoveLearningMethodService, MoveLearningMethodService>();
            services.AddScoped<IMoveLearningMethodRepository, MoveLearningMethodRepository>();

            return services;
        }
    }
}
