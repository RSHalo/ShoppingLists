using ShoppingList.Core.Helper;
using ShoppingList.Data.Azure.Products;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Products;

namespace ShoppingList.Data.Azure.Lists
{
    internal class ListRepository : Repository<IListEntity, ListEntity>, IListRepository
    {
        private const string PartitionKey = "lists";

        private readonly IProductRepository _productRepository;
        private readonly IItemRepository _itemRepository;

        public ListRepository(IProductRepository productRepository, IItemRepository itemRepository) : base("Lists")
        {
            _productRepository = productRepository;
            _itemRepository = itemRepository;
        }

        public async Task<IListEntity> FindListAsync(string name)
        {
            IListEntity list = await FindAsync(PartitionKey, name);
            if (list == null)
            {
                return null;
            }

            IList<IProductEntity> products = await _productRepository.AllForShopAsync(list.ShopName);
            IList<IItemEntity> listItems = await _itemRepository.AllInListAsync(name);

            foreach (IProductEntity product in products.InShopOrder())
            {
                IItemEntity item = listItems.FirstOrDefault(item => item.Name == product.Name);
                if (item == null)
                {
                    item = new ItemEntity
                    {
                        Name = product.Name,
                        ListName = name
                    };
                }
                else
                {
                    // The item is in the ListItems Azure table, which means the item is on the list.
                    item.IsOn = true;
                }

                // Attach the product information.
                item.Next = product.Next;
                item.IsFirst = product.IsFirst;

                list.Items.Add(item);
            }

            return list;
        }

        public Task<bool> AddItemAsync(string listName, string itemName)
        {
            return _itemRepository.AddAsync(listName, itemName);
        }

        public Task<bool> AddListAsync(string name, string shopName)
        {
            ListEntity list = new ListEntity
            {
                PartitionKey = PartitionKey,
                RowKey = name,
                ShopName = shopName
            };

            return AddEntityAsync(list);
        }

        public async Task<IList<string>> AllListsNamesAsync()
        {
            IList<IListEntity> lists = await AllAsync();
            return lists.Select(list => list.Name).ToList();
        }

        public Task<bool> ClearAsync(string listName, bool keepUnpickedItems)
        {
            return _itemRepository.DeleteAllInListAsync(listName, keepUnpickedItems);
        }

        public Task<bool> DeleteListAsync(string name)
        {
            return DeleteEntityAsync(PartitionKey, name);
        }

        public Task<bool> PickItemAsync(string listName, string itemName)
        {
            return _itemRepository.PickAsync(listName, itemName);
        }

        public Task<bool> RemoveItemAsync(string listName, string itemName)
        {
            return _itemRepository.DeleteAsync(listName, itemName);
        }

        public Task<bool> UnpickItemAsync(string listName, string itemName)
        {
            return _itemRepository.UnpickAsync(listName, itemName);
        }

        public async Task<IList<string>> ListNamesForShopAsync(string shopName)
        {
            IList<IListEntity> lists = await QueryAsync($"ShopName eq '{shopName}'");
            return lists.Select(list => list.Name).ToList();
        }
    }
}
