using ShoppingList.Data.Lists;

namespace ShoppingList.Data.InMemory.Lists
{
    internal class ListEntity : IListEntity
    {
        public ListEntity(string name, string shopName)
        {
            Name = name;
            ShopName = shopName;
        }

        public string Name { get; }

        public string ShopName { get; }

        public IList<IItemEntity> Items { get; set; }
    }
}
