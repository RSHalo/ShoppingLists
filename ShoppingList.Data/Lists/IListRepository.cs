namespace ShoppingList.Data.Lists
{
    public interface IListRepository
    {
        Task<IEnumerable<IListEntity>> AllListsAsync();

        Task<IListEntity> FindListAsync(string name);

        Task<bool> AddListAsync(string name, string shopName);

        Task<bool> DeleteListAsync(string name);
    }
}
