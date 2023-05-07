using ShoppingList.Data.Products;
using System.Runtime.Serialization;

namespace ShoppingList.Data.Azure.Products
{
    internal class ProductEntity : BaseTableEntity, IProductEntity
    {
        [IgnoreDataMember]
        public string ShopName
        {
            get => PartitionKey;
            set => PartitionKey = value;
        }

        [IgnoreDataMember]
        public string Name
        {
            get => RowKey;
            set => RowKey = value;
        }

        public string Next { get; set; }

        public bool IsFirst { get; set; }
    }
}
