namespace ShoppingList.Data.Lists
{
    public interface IItemEntity
    {
        string Name { get; }

        int Order { get; }

        bool IsPicked { get; }
    }
}
