using PokeSaveRomManager.Api.Types.Repositories;
using PokeSaveRomManager.Api.Types.Services;

namespace PokeSaveRomManager.Api.Types
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddgTypeServices(this IServiceCollection services)
        {
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<ITypeService, TypeService>();

            return services;
        }
    }
}
