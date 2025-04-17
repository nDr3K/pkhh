using PokeSaveRomManager.Api.Users.Repositories;
using PokeSaveRomManager.Api.Users.Services;

namespace PokeSaveRomManager.Api.Users
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
