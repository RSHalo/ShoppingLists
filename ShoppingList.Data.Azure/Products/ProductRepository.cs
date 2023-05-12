using ShoppingList.Data.Products;

namespace ShoppingList.Data.Azure.Products
{
    internal class ProductRepository : Repository<IProductEntity, ProductEntity>, IProductRepository
    {
        public ProductRepository() : base("Products")
        {

        }

        public Task<bool> AddAsync(string shopName, IProductEntity newProduct)
        {
            ProductEntity entity = new ProductEntity
            {
                ShopName = shopName,
                Name = newProduct.Name,
                Next = newProduct.Next,
                IsFirst = newProduct.IsFirst
            };

            return AddEntityAsync(entity);
        }

        public Task<bool> DeleteAsync(string shopName, string productName)
        {
            return DeleteEntityAsync(shopName, productName);
        }

        public Task<IList<IProductEntity>> AllForShopAsync(string shopName)
        {
            return AllInPartitionAsync(shopName);
        }

        public async Task<bool> UpdateAsync(string shopName, string productName, IProductEntity productData)
        {
            ProductEntity entity = await FindAsync(shopName, productName) as ProductEntity;
            entity.Next = productData.Next;
            entity.IsFirst = productData.IsFirst;

            return await MergeEntityAsync(entity);
        }
    }
}
