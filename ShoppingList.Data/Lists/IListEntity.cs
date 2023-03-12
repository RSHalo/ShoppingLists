namespace ShoppingList.Data.Lists
{
    public interface IListEntity
    {
        string ShopName { get; }

        string Name { get; }

        IList<IItemEntity> Items { get; }
    }
}
