using ShoppingList.Core.Products;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.InMemory.Products
{
    public class ProductMaintainer : ProductMaintainerBase
    {
        public ProductMaintainer(IShopRepository shopRepository, IListRepository listRepository)
            : base(shopRepository, listRepository)
        {
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

        protected override Task OnProductRemovedAsync(string shopName, string productName)
        {
            return UpdateProductsForShopLists(shopName);
        }

        private async Task UpdateProductsForShopLists(string shopName)
        {
            IList<IProductEntity> products = await _shopRepository.AllProductsForShop(shopName);

            IList<IListEntity> listsToUpdate = await _listRepository.AllListsForShop(shopName);
            foreach (IListEntity list in listsToUpdate)
            {
                await _listRepository.UpdateShopProducts(list.Name, products);
            }
        }
    }
}
