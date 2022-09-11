using ShoppingList.Data.Lists;

namespace ShoppingList.Data.Helper
{
    public static class ListHelper
    {
        public static IList<IItemEntity> Sort(this IEnumerable<IItemEntity> unorderedItems)
        {
            IItemEntity firstItem = null;
            Dictionary<string, IItemEntity> itemsByName = new Dictionary<string, IItemEntity>();

            foreach (IItemEntity item in unorderedItems)
            {
                if (item.IsFirst)
                {
                    firstItem = item;
                }

                itemsByName.Add(item.Name, item);
            }

            if (firstItem == null)
            {
                throw new Exception("No first item");
            }

            List<IItemEntity> orderedItems = new List<IItemEntity>
            {
                firstItem
            };

            string nextName = firstItem.Next;
            while (nextName != null)
            {
                IItemEntity nextItem = itemsByName[nextName];
                orderedItems.Add(nextItem);

                nextName = nextItem.Next;
            }

            return orderedItems;
        }
    }
}
