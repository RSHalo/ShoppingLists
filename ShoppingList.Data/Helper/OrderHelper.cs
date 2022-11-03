using ShoppingList.Data.Products;

namespace ShoppingList.Data.Helper
{
    public static class OrderHelper
    {
        public static IList<TItemEntity> InShopOrder<TItemEntity>(this IEnumerable<TItemEntity> unorderedItems) where TItemEntity : IProductEntity
        {
            if (unorderedItems.Any() == false)
            {
                return new List<TItemEntity>();
            }

            TItemEntity firstItem = default(TItemEntity);
            Dictionary<string, TItemEntity> itemsByName = new Dictionary<string, TItemEntity>();

            foreach (TItemEntity item in unorderedItems)
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

            List<TItemEntity> orderedItems = new List<TItemEntity>
            {
                firstItem
            };

            string nextName = firstItem.Next;
            while (nextName != null)
            {
                TItemEntity nextItem = itemsByName[nextName];
                orderedItems.Add(nextItem);

                nextName = nextItem.Next;
            }

            return orderedItems;
        }
    }
}
