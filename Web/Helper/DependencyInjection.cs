using ShoppingList.Data.InMemory.Lists;
using ShoppingList.Data.Lists;

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
