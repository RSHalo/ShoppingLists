using ShoppingList.Data.Lists;

namespace ShoppingList.Data.InMemory.Lists
{
    internal class ItemEntity : IItemEntity
    {
        public ItemEntity(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string Next { get; set; }

        public bool IsFirst { get; set; }

        public bool IsOn { get; set; }

        public bool IsPicked { get; set; }
    }
}
