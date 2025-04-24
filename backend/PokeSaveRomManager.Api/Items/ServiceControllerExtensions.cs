using PokeSaveRomManager.Api.Items.Repositories;
using PokeSaveRomManager.Api.Items.Services;

namespace PokeSaveRomManager.Api.Items
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddItemServices(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IItemRepository, ItemRepository>();

            return services;
        }
    }
}
