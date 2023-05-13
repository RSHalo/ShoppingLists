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
        Task<IList<IProductEntity>> AllProductsForShopAsync(string shopName);

        /// <summary>
        /// Adds a product to a shop.
        /// </summary>
        /// <param name="shopName">The name of the shop.</param>
        /// <param name="newProduct">The new product to add.</param>
        /// <returns></returns>
        Task<bool> AddProductAsync(string shopName, IProductEntity newProduct);

        /// <summary>
        /// Removes a product from a shop.
        /// </summary>
        /// <param name="shopName">The name of the shop.</param>
        /// <param name="toRemove">The product to remove.</param>
        Task<bool> RemoveProductAsync(string shopName, IProductEntity toRemove);

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="shopName">The name of the shop.</param>
        /// <param name="productName">The product to update.</param>
        /// <param name="productData">The new product data.</param>
        Task<bool> UpdateProductAsync(string shopName, string productName, IProductEntity productData);
    }
}
