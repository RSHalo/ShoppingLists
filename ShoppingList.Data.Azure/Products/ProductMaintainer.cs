using ShoppingList.Core.Products;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.Azure.Products
{
    internal class ProductMaintainer : ProductMaintainerBase
    {
        public ProductMaintainer(IShopRepository shopRepository) : base(shopRepository)
        {

        }
    }
}
