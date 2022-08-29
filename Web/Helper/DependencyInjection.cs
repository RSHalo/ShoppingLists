using ShoppingList.Data;
using ShoppingList.Data.InMemory;

namespace ShoppingList.Web.Helper
{
    public static class DependencyInjection
    {
        public static void AddDataAccessRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IListRepository, ListRepository>();
        }
    }
}
