namespace ShoppingList.Data
{
    public interface IListEntity
    {
        string Name { get; }

        IList<IItemEntity> Items { get; }
    }
}
