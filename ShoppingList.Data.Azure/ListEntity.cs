namespace ShoppingList.Data.InMemory
{
    internal class ListEntity : IListEntity
    {
        public string Name { get; set; }

        public IList<IItemEntity> Items { get; set; }
    }
}
