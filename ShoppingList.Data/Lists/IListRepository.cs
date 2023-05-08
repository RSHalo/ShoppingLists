using ShoppingList.Data.Products;

namespace ShoppingList.Data.Lists
{
    public interface IListRepository
    {
        Task<IList<IListEntity>> AllListsAsync();

        Task<IListEntity> FindListAsync(string name);

        Task<IList<IListEntity>> AllListsForShop(string shopName);

        Task<bool> UpdateShopProducts(string listName, IList<IProductEntity> allShopProducts);

        Task<bool> AddListAsync(string name, string shopName);

        Task<bool> DeleteListAsync(string name);

        Task<bool> AddItemAsync(string listName, string itemName);

        Task<bool> RemoveItemAsync(string listName, string itemName);

        Task<bool> PickItemAsync(string listName, string itemName);

        Task<bool> UnpickItemAsync(string listName, string itemName);

        Task<bool> ClearAsync(string listName, bool keepUnpickedItems);
    }
}
