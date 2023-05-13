using ShoppingList.Core.Helper;
using ShoppingList.Data.InMemory.Shops;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.InMemory.Lists
{
    public class ListRepository : IListRepository
    {
        private readonly List<ListItemCollection> _lists;
        private readonly IShopRepository _shopRepository;

        public ListRepository(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;

            ListItemCollection list1 = new ListItemCollection
            {
                ShopName = ShopNames.ALDI,
                Name = "ALDI List"
            };
            list1.AddItem(ProductNames.Bananas);
            list1.AddItem(ProductNames.Apples);
            list1.AddItem(ProductNames.Onions);
            list1.AddItem(ProductNames.Yoghurt);

            ListItemCollection list2 = new ListItemCollection
            {
                ShopName = ShopNames.Sainsburys,
                Name = "Berrys List"
            };
            list2.AddItem(ProductNames.Crisps);
            list2.AddItem(ProductNames.QuornNuggets);
            list2.AddItem(ProductNames.Sausages);

            _lists = new List<ListItemCollection>
            {
                list1, list2
            };
        }

        public Task<IList<string>> AllListsNamesAsync()
        {
            IList<string> listNames = _lists
                .Select(list => list.Name)
                .ToList();

            return Task.FromResult(listNames);
        }

        public async Task<IListEntity> FindListAsync(string name)
        {
            ListItemCollection list = FindList(name);
            if (list == null)
            {
                return null;
            }

            ListEntity listEntity = new ListEntity(name, list.ShopName);
            List<IItemEntity> listItems = new List<IItemEntity>();

            IList<IProductEntity> products = await _shopRepository.AllProductsForShopAsync(list.ShopName);
            foreach (IProductEntity product in products.InShopOrder())
            {
                IItemEntity item = list.Item(product.Name);
                if (item == null)
                {
                    item = new ItemEntity(product.Name);
                }
                else
                {
                    // The item is in the in-memory dictionary, which means the item is on the list.
                    item.IsOn = true;
                }

                // Attach the product information.
                item.Next = product.Next;
                item.IsFirst = product.IsFirst;

                listItems.Add(item);
            }

            listEntity.Items = listItems;
            return listEntity;
        }

        public Task<IList<string>> ListNamesForShopAsync(string shopName)
        {
            IList<string> listNames = _lists
                .Where(list => list.ShopName == shopName)
                .Select(list => list.Name)
                .ToList();

            return Task.FromResult(listNames);
        }

        public Task<bool> AddListAsync(string name, string shopName)
        {
            ListItemCollection newList = new ListItemCollection
            {
                Name = name,
                ShopName = shopName
            };
            
            _lists.Add(newList);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteListAsync(string name)
        {
            ListItemCollection list = FindList(name);
            _lists.Remove(list);

            return Task.FromResult(true);
        }

        public Task<bool> AddItemAsync(string listName, string itemName)
        {
            ListItemCollection list = FindList(listName);
            if (list != null)
            {
                list.AddItem(itemName);
            }

            return Task.FromResult(true);
        }

        public Task<bool> RemoveItemAsync(string listName, string itemName)
        {
            ListItemCollection list = FindList(listName);
            if (list != null)
            {
                list.RemoveItem(itemName);
            }

            return Task.FromResult(true);
        }

        public Task<bool> PickItemAsync(string listName, string itemName)
        {
            TogglePickStatus(listName, itemName, true);
            return Task.FromResult(true);
        }

        public Task<bool> UnpickItemAsync(string listName, string itemName)
        {
            TogglePickStatus(listName, itemName, false);
            return Task.FromResult(true);
        }

        private void TogglePickStatus(string listName, string itemName, bool newPickedStatus)
        {
            ListItemCollection list = FindList(listName);
            if (list != null)
            {
                list.TogglePickStatus(itemName, newPickedStatus);
            }
        }

        public Task<bool> ClearAsync(string listName, bool keepUnpickedItems)
        {
            ListItemCollection list = FindList(listName);
            if (list == null)
            {
                return Task.FromResult(false);
            }

            if (keepUnpickedItems)
            {
                List<string> toRemove = new List<string>();

                foreach (IItemEntity item in list.Items)
                {
                    if (item.IsPicked)
                    {
                        toRemove.Add(item.Name);
                    }
                }

                foreach (string itemName in toRemove)
                {
                    list.RemoveItem(itemName);
                }
            }
            else
            {
                list.Clear();
            }

            return Task.FromResult(true);
        }

        private ListItemCollection FindList(string name)
        {
            ListItemCollection list = _lists.FirstOrDefault(list => list.Name == name);
            if (list != null)
            {
                return list;
            }

            return null;
        }
    }
}
