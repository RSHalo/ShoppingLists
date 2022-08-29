namespace ShoppingList.Data.InMemory
{
    internal class ItemEntity : IItemEntity
    {
        public string Name { get; set; }

        public int Order { get; set; }
    }
}
