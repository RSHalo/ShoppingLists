using ShoppingList.Core.Products;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.Azure.Products
{
    internal class ProductMaintainer : ProductMaintainerBase
    {
        public ProductMaintainer(IShopRepository shopRepository, IListRepository listRepository)
            : base(shopRepository, listRepository)
        {

        }

        protected override async Task OnProductRemovedAsync(string shopName, string productName)
        {
            // Ensure any corresponding list items are deleted from the shop's lists.
            IList<IListEntity> lists = await _listRepository.AllListsForShop(shopName);
            foreach (IListEntity list in lists)
            {
                await _listRepository.RemoveItemAsync(list.Name, productName);
            }
        }
    }
}
