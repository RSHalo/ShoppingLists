using ShoppingList.Data.Lists;
using ShoppingList.Data.Products;

namespace ShoppingList.Data.InMemory.Lists
{
    internal class ItemEntity : IItemEntity
    {
        private readonly IProductEntity _product;

        public ItemEntity(IProductEntity product)
        {
            _product = product;
        }

        public string Name => _product.Name;

        public string Next
        {
            get => _product.Next;
            set => _product.Next = value;
        }

        public bool IsFirst => _product.IsFirst;

        public bool IsPicked { get; set; }

        public bool IsOn { get; set; }
    }
}
