using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Data.Shops
{
    public interface IShopRepository
    {
        Task<IList<IShopEntity>> AllShopsAsync();

        Task<IList<IProductEntity>> AllProductsForShop(string shopName);
    }
}
