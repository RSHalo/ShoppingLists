using ShoppingList.Data.Lists;

namespace ShoppingList.Data.Azure.Lists
{
    internal interface IItemRepository
    {
        Task<IList<IItemEntity>> AllInListAsync(string listName);

        Task<bool> DeleteAllInListAsync(string listName, bool keepUnpickedItems);

        Task<bool> AddAsync(string listName, string itemName);

        Task<bool> DeleteAsync(string listName, string itemName);

        Task<bool> PickAsync(string listName, string itemName);

        Task<bool> UnpickAsync(string listName, string itemName);
    }
}
