using ShoppingList.Data.Products;

namespace ShoppingList.Data.Lists
{
    /// <summary>
    /// An item that can be on a shopping list.
    /// </summary>
    public interface IItemEntity : IProductEntity
    {
        /// <summary>
        /// Whether the item is on the list.
        /// </summary>
        bool IsOn { get; set; }

        /// <summary>
        /// Whether the item has been picked during the current shopping trip.
        /// </summary>
        bool IsPicked { get; }
    }
}
