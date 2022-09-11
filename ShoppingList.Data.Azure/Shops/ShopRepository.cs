using ShoppingList.Data.Helper;
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

        public Task<IList<IShopEntity>> AllShopsAsync()
        {
            return Task.FromResult(_shops);
        }

        public Task<IList<IProductEntity>> AllProductsForShop(string shopName)
        {
            if (_productsByShop.ContainsKey(shopName))
            {
                IList<IProductEntity> products = _productsByShop[shopName].InShopOrder();
                return Task.FromResult(products);
            }

            throw new Exception("No shop.");
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
