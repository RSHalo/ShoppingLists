using ShoppingList.Data.Lists;

namespace ShoppingList.Data.InMemory.Lists
{
    internal class ListItemCollection
    {
        private readonly Dictionary<string, ItemEntity> _itemsByName = new Dictionary<string, ItemEntity>();

        public string Name { get; set; }

        public string ShopName { get; set; }

        public IEnumerable<IItemEntity> Items => _itemsByName.Values;

        public ItemEntity Item(string itemName)
        {
            if (_itemsByName.ContainsKey(itemName))
            {
                return _itemsByName[itemName];
            }

            return null;
        }

        public void AddItem(string itemName)
        {
            _itemsByName[itemName] = new ItemEntity(itemName);
        }

        public void RemoveItem(string itemName)
        {
            if (_itemsByName.ContainsKey(itemName))
            {
                _itemsByName.Remove(itemName);
            }
        }

        public void TogglePickStatus(string itemName, bool newPickedStatus)
        {
            ItemEntity item = Item(itemName);
            if (item != null)
            {
                item.IsPicked = newPickedStatus;
            }
        }

        public void Clear()
        {
            _itemsByName.Clear();
        }
    }
}
