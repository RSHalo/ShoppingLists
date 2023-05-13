using Azure;
using Azure.Data.Tables;
using ShoppingList.Core;

namespace ShoppingList.Data.Azure
{
    internal class Repository<TEntityInterface, TEntity>
        where TEntity : class, TEntityInterface, ITableEntity
    {
        private readonly TableClient _tableClient;

        public Repository(string tableName, IDataStoreOptions dataStoreOptions)
        {
            _tableClient = new TableClient(dataStoreOptions.AzureStorageConnectionString, tableName);
        }

        protected TableClient TableClient => _tableClient;

        protected async Task<IList<TEntityInterface>> QueryAsync(string filter = null)
        {
            AsyncPageable<TEntity> entities = _tableClient.QueryAsync<TEntity>(filter);
            return await ToListAsync(entities);
        }

        protected Task<IList<TEntityInterface>> AllAsync()
        {
            return QueryAsync();
        }

        protected AsyncPageable<TEntity> QueryByPartitionAsync(string partitionKey)
        {
            return _tableClient.QueryAsync<TEntity>(filter: $"PartitionKey eq '{partitionKey}'"); ;
        }

        protected async Task<IList<TEntityInterface>> AllInPartitionAsync(string partitionKey)
        {
            AsyncPageable<TEntity> entities = QueryByPartitionAsync(partitionKey);
            return await ToListAsync(entities);
        }

        protected async Task<bool> PerformBatchOperation(IEnumerable<TableTransactionAction> transactionActions)
        {
            Response<IReadOnlyList<Response>> responses = await _tableClient.SubmitTransactionAsync(transactionActions);
            foreach (Response response in responses.Value)
            {
                if (IsSuccess(response) == false)
                {
                    return false;
                }
            }

            return true;
        }

        protected async Task<TEntityInterface> FindAsync(string partitionKey, string rowKey)
        {
            NullableResponse<TEntity> response = await _tableClient.GetEntityIfExistsAsync<TEntity>(partitionKey, rowKey);
            if (response.HasValue)
            {
                return response.Value;
            }

            return default;
        }

        protected async Task<bool> AddEntityAsync(TEntity entity)
        {
            Response response = await _tableClient.AddEntityAsync(entity);
            return IsSuccess(response);
        }

        protected async Task<bool> DeleteEntityAsync(string partitionKey, string rowKey)
        {
            Response response = await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
            return IsSuccess(response);
        }

        protected async Task<bool> MergeEntityAsync(TEntity entity)
        {
            Response response = await _tableClient.UpdateEntityAsync(entity, entity.ETag);
            return IsSuccess(response);
        }

        protected async Task<bool> MergeEntityAsync(TableEntity entity)
        {
            Response response = await _tableClient.UpdateEntityAsync(entity, entity.ETag);
            return IsSuccess(response);
        }

        private async Task<IList<TEntityInterface>> ToListAsync(AsyncPageable<TEntity> items)
        {
            List<TEntityInterface> list = new List<TEntityInterface>();

            await foreach (TEntity item in items)
            {
                list.Add(item);
            }

            return list;
        }

        private static bool IsSuccess(Response response)
        {
            return response.IsError == false;
        }
    }
}
