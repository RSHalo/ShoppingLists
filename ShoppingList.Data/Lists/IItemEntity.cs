namespace ShoppingList.Data.Lists
{
    /// <summary>
    /// An item in a shopping list.
    /// </summary>
    public interface IItemEntity
    {
        /// <summary>
        /// The name of the item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The name of the next item in the list.
        /// </summary>
        string Next { get; }

        /// <summary>
        /// Whether the item is the first item in the list
        /// </summary>
        bool IsFirst { get; }

        /// <summary>
        /// Whether the item has been picked during the current shopping trip.
        /// </summary>
        bool IsPicked { get; }
    }
}
