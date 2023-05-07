using ShoppingList.Core.Helper;
using ShoppingList.Data.Azure.Products;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.Azure.Shops
{
    internal class ShopRepository : Repository<IShopEntity, ShopEntity>, IShopRepository
    {
        private const string PartitionKey = "shop";
        private readonly IProductRepository _productRepository;

        public ShopRepository(IProductRepository productRepository) : base("Shops")
        {
            _productRepository = productRepository;
        }

        public Task<IShopEntity> FindAsync(string shopName)
        {
            return FindAsync(PartitionKey, shopName);
        }

        public Task<IList<IShopEntity>> AllShopsAsync()
        {
            return AllAsync();
        }

        public async Task<IList<IProductEntity>> AllProductsForShop(string shopName)
        {
            IList<IProductEntity> products = await _productRepository.AllForShopAsync(shopName);
            return products.InShopOrder();
        }

        public Task<bool> AddProductAsync(string shopName, IProductEntity newProduct)
        {
            return _productRepository.AddAsync(shopName, newProduct);
        }

        public Task<bool> RemoveProductAsync(string shopName, IProductEntity toRemove)
        {
            return _productRepository.DeleteAsync(shopName, toRemove.Name);
        }

        public Task<bool> UpdateProductAsync(string shopName, string productName, IProductEntity productData)
        {
            return _productRepository.UpdateAsync(shopName, productName, productData);
        }
    }
}
