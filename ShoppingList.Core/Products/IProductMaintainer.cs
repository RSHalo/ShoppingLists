namespace ShoppingList.Core.Products
{
    /// <summary>
    /// Maintains products in shops.
    /// </summary>
    public interface IProductMaintainer
    {
        Task<bool> RegisterProductAsync(string shopName, string newProductName, string nextProductName);

        Task<bool> RemoveProductAsync(string shopName, string productName);
    }
}
