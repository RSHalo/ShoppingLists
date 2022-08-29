namespace ShoppingList.Data.InMemory
{
    public class ListRepository : IListRepository
    {
        private readonly List<ListEntity> _lists;

        public ListRepository()
        {
            _lists = new List<ListEntity>
            {
                new ListEntity
                {
                    Name = "ALDI",
                    Items = new List<IItemEntity>
                    {
                        new ItemEntity { Name = "apples", Order = 1 },
                        new ItemEntity { Name = "bananas", Order = 2 },
                        new ItemEntity { Name = "yoghurt", Order = 4 },
                        new ItemEntity { Name = "onions", Order = 6 }
                    }
                },
                new ListEntity
                {
                    Name = "Sainsbury's",
                    Items = new List<IItemEntity>
                    {
                        new ItemEntity { Name = "sausages", Order = 1 },
                        new ItemEntity { Name = "Walkers Crisps", Order = 2 },
                        new ItemEntity { Name = "Quorn Nug", Order = 8 },
                    }
                }
            };
        }

        public Task<IEnumerable<IListEntity>> AllListsAsync()
        {
            IEnumerable<IListEntity> lists = _lists.AsEnumerable();
            return Task.FromResult(lists);
        }

        public Task<IListEntity> FindListAsync(string name)
        {
            IListEntity list = _lists.FirstOrDefault(list => list.Name == name);
            return Task.FromResult(list);
        }
    }
}
