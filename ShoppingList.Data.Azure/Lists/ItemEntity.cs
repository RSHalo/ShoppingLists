using ShoppingList.Data.Lists;

namespace ShoppingList.Data.InMemory.Lists
{
    internal class ItemEntity : IItemEntity
    {
        public string Name { get; set; }

        public string Next { get; set; }

        public bool IsFirst { get; set; }

        public bool IsPicked { get; set; }
    }
}
