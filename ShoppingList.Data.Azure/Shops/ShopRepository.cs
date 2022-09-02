using ShoppingList.Data.Shops;

namespace ShoppingList.Data.InMemory.Shops
{
    internal class ShopRepository : IShopRepository
    {
        public Task<IList<IShopEntity>> AllShopsAsync()
        {
            IList<IShopEntity> shops = new List<IShopEntity>
            {
                new ShopEntity { Name = ShopNames.ALDI },
                new ShopEntity { Name = ShopNames.Sainsburys }
            };

            return Task.FromResult(shops);
        }

        public Task<IList<IProductEntity>> AllProductsForShop(string shopName)
        {
            throw new NotImplementedException();
        }
    }
}
