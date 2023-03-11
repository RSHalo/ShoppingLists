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

        public Task<IList<IProductEntity>> AllProductsForShop(string shopName)
        {
            if (_productsByShop.TryGetValue(shopName, out List<IProductEntity> products))
            {
                return Task.FromResult(products.InShopOrder());
            }

            throw new Exception("No shop.");
        }

        public Task<bool> RegisterProductAsync(string shopName, string newProductName, string nextProductName)
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

        public Task<bool> RemoveProductAsync(string shopName, string productName)
        {
            if (_productsByShop.ContainsKey(shopName) == false || productName == null)
            {
                return Task.FromResult(false);
            }

            List<IProductEntity> existingProducts = _productsByShop[shopName];
            bool result = RemoveProduct(shopName, existingProducts.InShopOrder(), productName);
            return Task.FromResult(result);
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
            if (string.IsNullOrWhiteSpace(nextProductName))
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

        private bool RemoveProduct(string shopName, IList<IProductEntity> existingProducts, string productToRemove)
        {
            IProductEntity previousProduct = null;
            IProductEntity toRemove = null;
            IProductEntity nextProduct = null;

            foreach (IProductEntity product in existingProducts)
            {
                if (toRemove != null)
                {
                    // We have found the product to remove, and the product it points to. We can exit the iteration.
                    nextProduct = product;
                    break;
                }

                if (product.Name == productToRemove)
                {
                    toRemove = product;
                }
                else
                {
                    previousProduct = product;
                }
            }

            if (toRemove != null)
            {
                existingProducts.Remove(toRemove);

                if (toRemove.IsFirst)
                {
                    // nextProduct will be null if toRemove was the only product in the list.
                    if (nextProduct != null)
                    {
                        nextProduct.IsFirst = true;
                    }
                }

                // If the previous product was pointing to the removed product, update the previous product's pointer.
                // toRemove.Next could be null if toRemove was the last product in the shop.
                if (previousProduct != null)
                {
                    previousProduct.Next = toRemove.Next;
                }

                _productsByShop[shopName] = existingProducts.ToList();
                return true;
            }

            return false;
        }
    }
}
