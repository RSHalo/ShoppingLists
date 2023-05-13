using ShoppingList.Core;
using ShoppingList.Core.Products;
using ShoppingList.Data.Azure;
using ShoppingList.Data.InMemory.Lists;
using ShoppingList.Data.InMemory.Shops;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Shops;

namespace ShoppingList.Web.Helper
{
    public static class DependencyInjection
    {
        public static void AddDataAccess(this IServiceCollection services, IDataStoreOptions dataStoreOptions)
        {
            services.AddSingleton<IProductMaintainer, ProductMaintainer>(); 

            if (dataStoreOptions.Type == "Azure")
            {
                services.AddAzureDataAccess();
            }
            else
            {
                services.AddSingleton<IListRepository, ListRepository>();
                services.AddSingleton<IShopRepository, ShopRepository>();
            }
        }
    }
}
