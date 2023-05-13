namespace ShoppingList.Core
{
    public interface IDataStoreOptions
    {
        string AzureStorageConnectionString { get; }

        string Type { get; }
    }
}