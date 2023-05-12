using Azure;
using Azure.Data.Tables;
using ShoppingList.Data.Lists;
using System.Collections.Concurrent;

namespace ShoppingList.Data.Azure.Lists
{
    internal class ItemRepository : Repository<IItemEntity, ItemEntity>, IItemRepository
    {
        public ItemRepository() : base("ListItems")
        {

        }

        public Task<IList<IItemEntity>> AllInListAsync(string listName)
        {
            return AllInPartitionAsync(listName);
        }

        public async Task<bool> DeleteAllInListAsync(string listName, bool keepUnpickedItems)
        {
            List<TableTransactionAction> deleteBatch = new List<TableTransactionAction>();
            AsyncPageable<ItemEntity> entities = QueryByPartitionAsync(listName);

            await foreach (ItemEntity entity in entities)
            {
                if (keepUnpickedItems == false || entity.IsPicked)
                {
                    deleteBatch.Add(new TableTransactionAction(TableTransactionActionType.Delete, entity));
                }
            }

            return await PerformBatchOperation(deleteBatch);
        }

        public Task<bool> AddAsync(string listName, string itemName)
        {
            ItemEntity item = new ItemEntity
            {
                PartitionKey = listName,
                RowKey = itemName
            };

            return AddEntityAsync(item);
        }

        public Task<bool> DeleteAsync(string listName, string itemName)
        {
            return DeleteEntityAsync(listName, itemName);
        }

        public Task<bool> PickAsync(string listName, string itemName)
        {
            return TogglePickedAsync(listName, itemName, newPickedState: true);
        }

        public Task<bool> UnpickAsync(string listName, string itemName)
        {
            return TogglePickedAsync(listName, itemName, newPickedState: false);
        }

        private async Task<bool> TogglePickedAsync(string listName, string itemName, bool newPickedState)
        {
            TableEntity entity = new TableEntity(listName, itemName)
            {
                ETag = ETag.All
            };
            entity.Add(nameof(IItemEntity.IsPicked), newPickedState);

            return await MergeEntityAsync(entity);
        }
    }
}
