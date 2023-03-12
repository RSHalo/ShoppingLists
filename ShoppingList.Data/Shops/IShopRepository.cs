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
        /// <param name="nextProductName">The next product in order. Null if <paramref name="newProductName"/> should be the last item in the shop.</param>
        Task<bool> RegisterProductAsync(string shopName, string newProductName, string nextProductName);

        /// <summary>
        /// Removes a product from a shop.
        /// </summary>
        /// <param name="shopName">The name of the shop.</param>
        /// <param name="productName">The name of the product to remove.</param>
        Task<bool> RemoveProductAsync(string shopName, string productName);
    }
}
