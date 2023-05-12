using ShoppingList.Data.Products;

namespace ShoppingList.Core.Products
{
    public class ProductDto : IProductEntity
    {
        public ProductDto(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string Next { get; set; }

        public bool IsFirst { get; set; }
    }
}
