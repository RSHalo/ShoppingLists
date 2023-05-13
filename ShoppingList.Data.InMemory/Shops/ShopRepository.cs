using ShoppingList.Core.Helper;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.InMemory.Shops
{
    public class ShopRepository : IShopRepository
    {
        private readonly IList<IShopEntity> _shops;
        private readonly Dictionary<string, List<IProductEntity>> _productsByShop = new Dictionary<string, List<IProductEntity>>();

        public ShopRepository()
        {
            _shops = new List<IShopEntity>
            {
                new ShopEntity { Name = ShopNames.ALDI },
                new ShopEntity { Name = ShopNames.Sainsburys }
            };

            InitializeProducts();
        }

        public Task<IShopEntity> FindAsync(string shopName)
        {
            return Task.FromResult(_shops.FirstOrDefault(shop => shop.Name == shopName));
        }

        public Task<IList<IShopEntity>> AllShopsAsync()
        {
            return Task.FromResult(_shops);
        }

        public Task<IList<IProductEntity>> AllProductsForShopAsync(string shopName)
        {
            if (_productsByShop.TryGetValue(shopName, out List<IProductEntity> products))
            {
                return Task.FromResult(products.InShopOrder());
            }

            throw new Exception("No shop.");
        }

        public Task<bool> AddProductAsync(string shopName, IProductEntity newProduct)
        {
            _productsByShop[shopName].Add(newProduct);
            return Task.FromResult(true);
        }

        public Task<bool> RemoveProductAsync(string shopName, IProductEntity toRemove)
        {
            if (_productsByShop.ContainsKey(shopName) == false || toRemove == null)
            {
                return Task.FromResult(false);
            }

            _productsByShop[shopName].Remove(toRemove);

            return Task.FromResult(true);
        }

        public Task<bool> UpdateProductAsync(string shopName, string productName, IProductEntity productData)
        {
            IProductEntity product = _productsByShop[shopName].Single(product => product.Name == productName);
            product.Next = productData.Next;
            product.IsFirst = productData.IsFirst;

            return Task.FromResult(true);
        }

        private void InitializeProducts()
        {
            List<IProductEntity> products = new List<IProductEntity>
            {
                new ProductEntity(ProductNames.Sausages)
                {
                    IsFirst = true,
                    Next = ProductNames.Bananas
                },
                new ProductEntity(ProductNames.Crisps)
                {
                    Next = ProductNames.QuornNuggets
                },
                new ProductEntity(ProductNames.Onions)
                {
                    Next = ProductNames.Crisps
                },
                new ProductEntity(ProductNames.QuornNuggets),
                new ProductEntity(ProductNames.Bananas)
                {
                    Next = ProductNames.Onions
                }
            };

            _productsByShop.Add(ShopNames.Sainsburys, products);

            products = new List<IProductEntity>
            {
                new ProductEntity(ProductNames.Bananas)
                {
                    Next = ProductNames.Yoghurt
                },
                new ProductEntity(ProductNames.Yoghurt)
                {
                    Next = ProductNames.Onions
                },
                new ProductEntity(ProductNames.Fish),
                new ProductEntity(ProductNames.Apples)
                {
                    IsFirst = true,
                    Next = ProductNames.Bananas
                },
                new ProductEntity(ProductNames.Onions)
                {
                    Next = ProductNames.Fish
                }
            };

            _productsByShop.Add(ShopNames.ALDI, products);
        }
    }
}
