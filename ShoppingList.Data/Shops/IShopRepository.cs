using ShoppingList.Data.Products;

namespace ShoppingList.Data.Shops
{
    public interface IShopRepository
    {
        Task<IList<IShopEntity>> AllShopsAsync();

        Task<IList<IProductEntity>> AllProductsForShop(string shopName);
    }
}
