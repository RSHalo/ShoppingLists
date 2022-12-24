using Microsoft.CodeAnalysis.CSharp.Syntax;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;
using ShoppingList.Web.Models.Lists;
using ShoppingList.Web.Models.Shops;

namespace ShoppingList.Web.Helper
{
    /// <summary>Maps entities to models.</summary>
    public static class ModelMapper
    {
        public static ShopModel ToShopModel(this IShopEntity entity)
        {
            return new ShopModel
            {
                Name = entity.Name
            };
        }

        public static ProductModel ToProductModel(this IProductEntity entity)
        {
            return new ProductModel
            {
                IsFirst = entity.IsFirst,
                Name = entity.Name,
                Next = entity.Next
            };
        }

        public static ListModel ToListModel(this IListEntity entity)
        {
            return new ListModel
            {
                Name = entity.Name,
                ShopName = entity.ShopName,
                Items = entity.Items.Select(ToItemModel).ToList()
            };

        }

        public static ItemModel ToItemModel(this IItemEntity itemEntity)
        {
            return new ItemModel
            {
                Name = itemEntity.Name,
                IsOn = itemEntity.IsOn,
                IsPicked = itemEntity.IsPicked
            };
        }
    }
}
