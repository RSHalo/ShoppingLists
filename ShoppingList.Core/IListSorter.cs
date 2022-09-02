using ShoppingList.Data;

namespace ShoppingList.Core
{
    public interface IListSorter
    {
        IList<IItemEntity> Sort(IListEntity shoppingList);
    }
}