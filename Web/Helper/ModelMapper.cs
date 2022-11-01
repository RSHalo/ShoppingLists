using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;
using ShoppingList.Web.Models;
using ShoppingList.Web.Models.Shops;

namespace ShoppingList.Web.Helper
{
    /// <summary>Maps entities to models.</summary>
    public static class ModelMapper
    {
        public static ShopModel ToModel(this IShopEntity entity)
        {
            return new ShopModel
            { 
                Name = entity.Name
            };
        }

        public static ProductModel ToModel(this IProductEntity entity)
        {
            return new ProductModel
            {
                IsFirst = entity.IsFirst,
                Name = entity.Name,
                Next = entity.Next
            };
        }
    }
}
