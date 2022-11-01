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
            if (_productsByShop.TryGetValue(shopName, out List<IProductEntity> products))
            {
                return Task.FromResult(products.InShopOrder());
            }

            throw new Exception("No shop.");
        }

        public Task<bool> RegisterProduct(string shopName, string newProductName, string nextProductName)
        {
            if (_productsByShop.ContainsKey(shopName) == false || newProductName == null)
            {
                return Task.FromResult(false);
            }

            List<IProductEntity> existingProducts = _productsByShop[shopName];
            ProductEntity newProduct = CreateProductToRegister(newProductName, nextProductName, existingProducts);
            _productsByShop[shopName].Add(newProduct);

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

        private ProductEntity CreateProductToRegister(string newProductName, string nextProductName, IList<IProductEntity> existingProducts)
        {
            if (nextProductName == null)
            {
                // The new product is the last item in the list. The current last item must point to the new item.
                IProductEntity lastItem = existingProducts.Single(product => product.Next == null);
                lastItem.Next = newProductName;

                return new ProductEntity(newProductName);
            }
            else
            {
                bool isFirst = false;
                // previousProduct needs to point to the new product.
                ProductEntity previousProduct = null;
                // The new product needs to point to nextProduct.
                ProductEntity nextProduct = null;

                foreach (ProductEntity existingProduct in existingProducts)
                {
                    if (existingProduct.Name == nextProductName)
                    {
                        nextProduct = existingProduct;
                    }
                    else if (existingProduct.Next == nextProductName)
                    {
                        previousProduct = existingProduct;
                    }
                }

                if (previousProduct != null)
                {
                    previousProduct.Next = newProductName;
                }
                else
                {
                    // Nothing currently points to nextProduct, which means that the new product is being inserted at the start.
                    isFirst = true;
                }

                return new ProductEntity(newProductName)
                {
                    IsFirst = isFirst,
                    Next = nextProduct.Name
                };
            }
        }
    }
}
