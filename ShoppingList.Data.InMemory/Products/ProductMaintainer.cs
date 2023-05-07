using ShoppingList.Core.Products;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.InMemory.Products
{
    public class ProductMaintainer : ProductMaintainerBase
    {
        private readonly IListRepository _listRepository;

        public ProductMaintainer(IShopRepository shopRepository, IListRepository listRepository)
            : base(shopRepository)
        {
            _listRepository = listRepository;
        }

        public override async Task<bool> RegisterProductAsync(string shopName, string newProductName, string nextProductName)
        {
            bool success = await base.RegisterProductAsync(shopName, newProductName, nextProductName);
            if (success)
            {
                await UpdateProductsForShopLists(shopName);
            }

            return success;
        }

        public override async Task<bool> RemoveProductAsync(string shopName, string productName)
        {
            bool success = await base.RemoveProductAsync(shopName, productName);
            if (success)
            {
                await UpdateProductsForShopLists(shopName);
            }

            return success;
        }

        private async Task UpdateProductsForShopLists(string shopName)
        {
            IList<IProductEntity> products = await ShopRepository.AllProductsForShop(shopName);

            IList<IListEntity> listsToUpdate = await _listRepository.AllListsForShop(shopName);
            foreach (IListEntity list in listsToUpdate)
            {
                await _listRepository.UpdateShopProducts(list.Name, products);
            }
        }
    }
}
