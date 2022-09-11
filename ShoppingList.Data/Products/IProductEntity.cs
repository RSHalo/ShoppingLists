namespace ShoppingList.Data.Products
{
    /// <summary>
    /// A product in a shop.
    /// </summary>
    public interface IProductEntity
    {
        /// <summary>
        /// The name of the product.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The name of the next item.
        /// </summary>
        string Next { get; }

        /// <summary>
        /// Whether the item is the first item.
        /// </summary>
        bool IsFirst { get; }
    }
}
