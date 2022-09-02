using ShoppingList.Data.Lists;

namespace ShoppingList.Data.InMemory.Lists
{
    internal class ItemEntity : IItemEntity
    {
        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsPicked { get; set; }
    }
}
