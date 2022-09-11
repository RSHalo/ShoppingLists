using ShoppingList.Data.Products;

namespace ShoppingList.Data.InMemory.Shops
{
    internal class ProductEntity : IProductEntity
    {
        public ProductEntity(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string Next { get; set; }

        public bool IsFirst { get; set; }
    }
}
