using ShoppingList.Data.InMemory.Shops;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.InMemory.Lists
{
    public class ListRepository : IListRepository
    {
        private readonly List<IListEntity> _lists;
        private readonly IShopRepository _shopRepository;

        public ListRepository(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;

            ListEntity list1 = new ListEntity
            {
                ShopName = ShopNames.ALDI,
                Name = "ALDI List",
                Items = _shopRepository.AllProductsForShop(ShopNames.ALDI).Result.Select(p => new ItemEntity(p)).ToList<IItemEntity>()
            };
            ToggleItem(list1, ProductNames.Bananas, true);
            ToggleItem(list1, ProductNames.Apples, true);
            ToggleItem(list1, ProductNames.Onions, true);
            ToggleItem(list1, ProductNames.Yoghurt, true);

            ListEntity list2 = new ListEntity
            {
                ShopName = ShopNames.Sainsburys,
                Name = "Berrys List",
                Items = _shopRepository.AllProductsForShop(ShopNames.Sainsburys).Result.Select(p => new ItemEntity(p)).ToList<IItemEntity>()
            };
            ToggleItem(list2, ProductNames.Crisps, true);
            ToggleItem(list2, ProductNames.QuornNuggets, true);
            ToggleItem(list2, ProductNames.Sausages, true);

            _lists = new List<IListEntity>
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
            IListEntity list = _lists.FirstOrDefault(list => list.Name == name);
            return Task.FromResult(list);
        }

        public Task<IList<IListEntity>> AllListsForShop(string shopName)
        {
            IList<IListEntity> lists = _lists.Where(list => list.ShopName == shopName).ToList();
            return Task.FromResult(lists);
        }

        public async Task<bool> UpdateShopProducts(string listName, IList<IProductEntity> allShopProducts)
        {
            ListEntity list = await FindListAsync(listName) as ListEntity;
            if (list != null)
            {
                // Key the old items by name.
                Dictionary<string, IItemEntity> oldItems = list.Items.ToDictionary(item => item.Name, item => item);

                // Register the new items with the list.
                list.Items = allShopProducts.Select(p => new ItemEntity(p)).ToList<IItemEntity>();

                // Re-apply the IsOn and IsPicked states to the new items.
                foreach (IItemEntity item in list.Items)
                {
                    if (oldItems.TryGetValue(item.Name, out IItemEntity oldItem))
                    {
                        item.IsOn = oldItem.IsOn;
                        item.IsPicked = oldItem.IsPicked;
                    }
                }
            }

            return true;
        }

        public Task<bool> AddListAsync(string name, string shopName)
        {
            ListEntity newList = new ListEntity
            {
                Name = name,
                ShopName = shopName,
                Items = _shopRepository.AllProductsForShop(shopName).Result.Select(p => new ItemEntity(p)).ToList<IItemEntity>()
            };
            
            _lists.Add(newList);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteListAsync(string name)
        {
            IListEntity list = _lists.FirstOrDefault(list => list.Name == name);
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

        public Task<bool> PickItemAsync(string listName, string itemName)
        {
            bool result = TogglePickStatus(listName, itemName, true);
            return Task.FromResult(result);
        }

        public Task<bool> UnpickItemAsync(string listName, string itemName)
        {
            bool result = TogglePickStatus(listName, itemName, false);
            return Task.FromResult(result);
        }

        private bool ToggleItem(string listName, string itemName, bool includeInList)
        {
            IListEntity list = _lists.FirstOrDefault(list => list.Name == listName);
            if (list != null)
            {
                return ToggleItem(list, itemName, includeInList);
            }

            return false;
        }

        private static bool ToggleItem(IListEntity list, string itemName, bool includeInList)
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

        private bool TogglePickStatus(string listName, string itemName, bool markAsPicked)
        {
            IListEntity list = _lists.FirstOrDefault(list => list.Name == listName);
            if (list != null)
            {
                return ToggleItemPickStatus(list, itemName, markAsPicked);
            }

            return false;
        }

        private static bool ToggleItemPickStatus(IListEntity list, string itemName, bool markAsPicked)
        {
            foreach (IItemEntity item in list.Items)
            {
                if (item.Name == itemName)
                {
                    item.IsPicked = markAsPicked;
                    return true;
                }
            }

            return false;
        }
    }
}
