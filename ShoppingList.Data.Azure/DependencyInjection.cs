using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Data.Azure.Lists;
using ShoppingList.Data.Azure.Products;
using ShoppingList.Data.Azure.Shops;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.Azure
{
    public static class DependencyInjection
    {
        public static void AddAzureDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IItemRepository, ItemRepository>();
            services.AddSingleton<IListRepository, ListRepository>();
            services.AddSingleton<IShopRepository, ShopRepository>();
        }
    }
}
