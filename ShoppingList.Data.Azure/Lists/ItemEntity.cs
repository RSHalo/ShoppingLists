using ShoppingList.Data.Lists;
using System.Runtime.Serialization;

namespace ShoppingList.Data.Azure.Lists
{
    internal class ItemEntity : BaseTableEntity, IItemEntity
    {
        [IgnoreDataMember]
        public string ListName
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

        public bool IsPicked { get; set; }

        [IgnoreDataMember]
        public bool IsOn { get; set; }

        [IgnoreDataMember]
        public string Next { get; set; }

        [IgnoreDataMember]
        public bool IsFirst { get; set; }
    }
}
