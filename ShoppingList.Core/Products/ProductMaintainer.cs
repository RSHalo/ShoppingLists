using ShoppingList.Data.Lists;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;

namespace ShoppingList.Core.Products
{
    public class ProductMaintainer : IProductMaintainer
    {
        protected readonly IShopRepository _shopRepository;
        protected readonly IListRepository _listRepository;

        public ProductMaintainer(IShopRepository shopRepository, IListRepository listRepository)
        {
            _shopRepository = shopRepository;
            _listRepository = listRepository;
        }

        public async Task<bool> RegisterProductAsync(string shopName, string newProductName, string nextProductName)
        {
            IList<IProductEntity> existingProducts = await _shopRepository.AllProductsForShop(shopName);
            IProductEntity newProduct = await CreateProductToRegisterAsync(shopName, newProductName, nextProductName, existingProducts);
            bool success = await _shopRepository.AddProductAsync(shopName, newProduct);

            return success;
        }

        public async Task<bool> RemoveProductAsync(string shopName, string productName)
        {
            IList<IProductEntity> existingProducts = await _shopRepository.AllProductsForShop(shopName);
            bool success = await RemoveProductAsync(shopName, existingProducts, productName);
            if (success)
            {
                // Ensure any corresponding list items are deleted from the shop's lists.
                await RemoveProductFromListsAsync(shopName, productName);
            }

            return success;
        }

        private async Task RemoveProductFromListsAsync(string shopName, string productName)
        {
            IList<string> lists = await _listRepository.ListNamesForShopAsync(shopName);
            foreach (string list in lists)
            {
                await _listRepository.RemoveItemAsync(list, productName);
            }
        }

        private async Task<ProductDto> CreateProductToRegisterAsync(string shopName, string newProductName, string nextProductName, IList<IProductEntity> existingProducts)
        {
            if (string.IsNullOrWhiteSpace(nextProductName))
            {
                // The new product is the last item in the list. The current last item must point to the new item.
                IProductEntity lastItem = existingProducts.Single(product => product.Next == null);
                lastItem.Next = newProductName;
                await _shopRepository.UpdateProductAsync(shopName, lastItem.Name, lastItem);

                return new ProductDto(newProductName);
            }
            else
            {
                bool isFirst = false;
                // previousProduct needs to point to the new product.
                IProductEntity previousProduct = null;
                // The new product needs to point to nextProduct.
                IProductEntity nextProduct = null;

                foreach (IProductEntity existingProduct in existingProducts)
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

                if (previousProduct == null)
                {
                    // Nothing currently points to nextProduct, which means that the new product is being inserted at the start.
                    isFirst = true;

                    // Ensure the current first item is no longer marked as IsFirst.
                    nextProduct.IsFirst = false;
                    await _shopRepository.UpdateProductAsync(shopName, nextProductName, nextProduct);
                }
                else
                {
                    previousProduct.Next = newProductName;
                    await _shopRepository.UpdateProductAsync(shopName, previousProduct.Name, previousProduct);
                }

                return new ProductDto(newProductName)
                {
                    IsFirst = isFirst,
                    Next = nextProduct.Name
                };
            }
        }

        private async Task<bool> RemoveProductAsync(string shopName, IList<IProductEntity> existingProducts, string productToRemove)
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
                await _shopRepository.RemoveProductAsync(shopName, toRemove);

                if (toRemove.IsFirst)
                {
                    // nextProduct will be null if toRemove was the only product in the list.
                    if (nextProduct != null)
                    {
                        nextProduct.IsFirst = true;
                        await _shopRepository.UpdateProductAsync(shopName, nextProduct.Name, nextProduct);
                    }
                }

                // If the previous product was pointing to the removed product, update the previous product's pointer.
                // toRemove.Next could be null if toRemove was the last product in the shop.
                if (previousProduct != null)
                {
                    previousProduct.Next = toRemove.Next;
                    await _shopRepository.UpdateProductAsync(shopName, previousProduct.Name, previousProduct);
                }

                return true;
            }

            return false;
        }
    }
}
