using ShoppingList.Data.Products;

namespace ShoppingList.Data.Shops
{
    public interface IShopRepository
    {
        /// <summary>
        /// Finds a shop by it's name.
        /// </summary>
        /// <param name="shopName">The name of the shop.</param>
        Task<IShopEntity> FindAsync(string shopName);

        /// <summary>
        /// Gets all shops.
        /// </summary>
        Task<IList<IShopEntity>> AllShopsAsync();

        /// <summary>
        /// Gets all products for a shop.
        /// </summary>
        /// <param name="shopName">The name of the shop.</param>
        Task<IList<IProductEntity>> AllProductsForShop(string shopName);

        /// <summary>
        /// Registers a product with a shop.
        /// </summary>
        /// <param name="shopName">The name of the shop.</param>
        /// <param name="newProductName">The new product to register.</param>
        /// <param name="nextProductName">The next product in order. Null if <paramref name="product"/> should be the last item in the shop.</param>
        Task<bool> RegisterProduct(string shopName, string newProductName, string nextProductName);
    }
}
