using ShoppingList.Data.Lists;
using System.Runtime.Serialization;

namespace ShoppingList.Data.Azure.Lists
{
    internal class ListEntity : BaseTableEntity, IListEntity
    {
        [IgnoreDataMember]
        public string Name => RowKey;

        public string ShopName { get; set; }

        [IgnoreDataMember]
        public IList<IItemEntity> Items { get; set; } = new List<IItemEntity>();
    }
}
