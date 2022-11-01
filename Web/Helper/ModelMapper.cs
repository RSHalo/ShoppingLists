using ShoppingList.Data.Products;
using ShoppingList.Web.Models;

namespace ShoppingList.Web.Helper
{
    /// <summary>Maps entities to models.</summary>
    public static class ModelMapper
    {
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
