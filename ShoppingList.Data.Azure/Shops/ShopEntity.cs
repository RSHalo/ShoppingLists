using ShoppingList.Data.Shops;
using System.Runtime.Serialization;

namespace ShoppingList.Data.Azure.Shops
{
    public class ShopEntity : BaseTableEntity, IShopEntity
    {
        [IgnoreDataMember]
        public string Name
        {
            get => RowKey;
            set => RowKey = value;
        }
    }
}
