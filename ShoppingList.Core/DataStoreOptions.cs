namespace ShoppingList.Core
{
    public class DataStoreOptions : IDataStoreOptions
    {
        public const string SectionKey = "DataStore";

        public string Type { get; set; }

        public string AzureStorageConnectionString { get; set; }
    }
}
