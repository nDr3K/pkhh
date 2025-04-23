using PokeSaveRomManager.Api.Categories.Repositories;
using PokeSaveRomManager.Api.Categories.Services;

namespace PokeSaveRomManager.Api.Categories
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddCategoryServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
