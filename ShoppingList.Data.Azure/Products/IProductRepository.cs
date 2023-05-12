using ShoppingList.Data.Products;

namespace ShoppingList.Data.Azure.Products
{
    internal interface IProductRepository
    {
        Task<IList<IProductEntity>> AllForShopAsync(string shopName);

        Task<bool> AddAsync(string shopName, IProductEntity newProduct);

        Task<bool> DeleteAsync(string shopName, string productName);

        Task<bool> UpdateAsync(string shopName, string productName, IProductEntity productData);
    }
}
