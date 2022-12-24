using ShoppingList.Data.InMemory.Shops;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.InMemory.Lists
{
    public class ListRepository : IListRepository
    {
        private readonly List<ListEntity> _lists;

        public ListRepository(IShopRepository shopRepository)
        {
            ListEntity list1 = new ListEntity
            {
                ShopName = ShopNames.ALDI,
                Name = "ALDI List",
                Items = shopRepository.AllProductsForShop(ShopNames.ALDI).Result.Select(p => new ItemEntity(p)).ToList<IItemEntity>()
            };
            ToggleItem(list1, ProductNames.Bananas, true);
            ToggleItem(list1, ProductNames.Apples, true);
            ToggleItem(list1, ProductNames.Onions, true);
            ToggleItem(list1, ProductNames.Yoghurt, true);

            ListEntity list2 = new ListEntity
            {
                ShopName = ShopNames.Sainsburys,
                Name = "Berrys List",
                Items = shopRepository.AllProductsForShop(ShopNames.Sainsburys).Result.Select(p => new ItemEntity(p)).ToList<IItemEntity>()
            };
            ToggleItem(list2, ProductNames.Crisps, true);
            ToggleItem(list2, ProductNames.QuornNuggets, true);
            ToggleItem(list2, ProductNames.Sausages, true);

            _lists = new List<ListEntity>
            {
                list1, list2
            };
        }

        public Task<IEnumerable<IListEntity>> AllListsAsync()
        {
            IEnumerable<IListEntity> lists = _lists.AsEnumerable();
            return Task.FromResult(lists);
        }

        public Task<IListEntity> FindListAsync(string name)
        {
            ListEntity list = _lists.FirstOrDefault(list => list.Name == name);
            return Task.FromResult(list as IListEntity);
        }

        public Task<bool> AddListAsync(string name, string shopName)
        {
            ListEntity newList = new ListEntity
            {
                Name = name,
                ShopName = shopName
            };
            
            _lists.Add(newList);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteListAsync(string name)
        {
            ListEntity list = _lists.FirstOrDefault(list => list.Name == name);
            _lists.Remove(list);

            return Task.FromResult(true);
        }

        public Task<bool> AddItemAsync(string listName, string itemName)
        {
            bool result = ToggleItem(listName, itemName, true);
            return Task.FromResult(result);
        }

        public Task<bool> RemoveItemAsync(string listName, string itemName)
        {
            bool result = ToggleItem(listName, itemName, false);
            return Task.FromResult(result);
        }

        private bool ToggleItem(string listName, string itemName, bool includeInList)
        {
            ListEntity list = _lists.FirstOrDefault(list => list.Name == listName);
            if (list != null)
            {
                return ToggleItem(list, itemName, includeInList);
            }

            return false;
        }

        private bool ToggleItem(IListEntity list, string itemName, bool includeInList)
        {
            foreach (IItemEntity item in list.Items)
            {
                if (item.Name == itemName)
                {
                    item.IsOn = includeInList;
                    return true;
                }
            }

            return false;
        }
    }
}
