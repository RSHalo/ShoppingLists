namespace ShoppingList.Data
{
    public interface IListRepository
    {
        Task<IEnumerable<IListEntity>> AllListsAsync();

        Task<IListEntity> FindListAsync(string name);
    }
}
