using Azure;
using Azure.Data.Tables;

namespace ShoppingList.Data.Azure
{
    internal class Repository<TEntityInterface, TEntity>
        where TEntity : class, TEntityInterface, ITableEntity
    {
        private readonly string _connectionString = @"AccountName=shoppinglistdev;AccountKey=Cprma55OlUVF1sly6I8ShyhGrScBivhWueTclrqEKp9WaKFzhVC6YND4hjiUNJkiQFhaDsbC+3Kl+AStsIkjZQ==;EndpointSuffix=core.windows.net;DefaultEndpointsProtocol=https;";
        private readonly TableClient _tableClient;

        public Repository(string tableName)
        {
            _tableClient = new TableClient(_connectionString, tableName);
        }

        protected TableClient TableClient => _tableClient;

        protected async Task<IList<TEntityInterface>> AllAsync()
        {
            AsyncPageable<TEntity> entities = _tableClient.QueryAsync<TEntity>();
            return await ToListAsync(entities);
        }

        protected async Task<IList<TEntityInterface>> AllInPartition(string partitionKey)
        {
            AsyncPageable<TEntity> entities = _tableClient.QueryAsync<TEntity>(filter: $"PartitionKey eq '{partitionKey}'");
            return await ToListAsync(entities);
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
