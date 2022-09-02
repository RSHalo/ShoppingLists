using ShoppingList.Data.Lists;

namespace ShoppingList.Data.InMemory.Lists
{
    internal class ListEntity : IListEntity
    {
        public string ShopName { get; set; }

        public string Name { get; set; }

        public IList<IItemEntity> Items { get; set; }
    }
}
