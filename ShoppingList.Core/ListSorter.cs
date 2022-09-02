using ShoppingList.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core
{
    public class ListSorter : IListSorter
    {
        public IList<IItemEntity> Sort(IListEntity shoppingList)
        {
            List<IItemEntity> sortedItems = new List<IItemEntity>();

            foreach (IItemEntity item in shoppingList.Items)
            {
                if (sortedItems.Any() == false)
                {
                    sortedItems.Add(item);
                }
            }

            return sortedItems;
        }
    }
}
