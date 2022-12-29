using ShoppingList.Data.Lists;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;

namespace ShoppingList.Core
{
    public class ProductMaintainer : IProductMaintainer
    {
        private readonly IShopRepository _shopRepository;
        private readonly IListRepository _listRepository;

        public ProductMaintainer(IShopRepository shopRepository, IListRepository listRepository)
        {
            _shopRepository = shopRepository;
            _listRepository = listRepository;
        }

        public async Task<bool> RegisterProductAsync(string shopName, string newProductName, string nextProductName)
        {
            bool success = await _shopRepository.RegisterProductAsync(shopName, newProductName, nextProductName);
            if (success)
            {
                IList<IProductEntity> products = await _shopRepository.AllProductsForShop(shopName);

                IList<IListEntity> listsToUpdate = await _listRepository.AllListsForShop(shopName);
                foreach (IListEntity list in listsToUpdate)
                {
                    await _listRepository.UpdateShopProducts(list.Name, products);
                }
            }

            return success;
        }
    }
}
