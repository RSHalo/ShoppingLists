using ShoppingList.Data.InMemory.Lists;
using ShoppingList.Data.InMemory.Shops;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Shops;

namespace ShoppingList.Web.Helper
{
    public static class DependencyInjection
    {
        public static void AddDataAccessRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IListRepository, ListRepository>();
            services.AddSingleton<IShopRepository, ShopRepository>();
        }
    }
}
