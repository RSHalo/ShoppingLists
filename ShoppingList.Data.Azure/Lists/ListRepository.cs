using ShoppingList.Data.Helper;
using ShoppingList.Data.InMemory.Shops;
using ShoppingList.Data.Lists;

namespace ShoppingList.Data.InMemory.Lists
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
                    ShopName = ShopNames.ALDI,
                    Name = "ALDI List",
                    Items = new List<IItemEntity>
                    {
                        new ItemEntity { Name = ProductNames.Bananas, Next = ProductNames.Yoghurt },
                        new ItemEntity { Name = ProductNames.Apples, Next =  ProductNames.Bananas, IsFirst = true },
                        new ItemEntity { Name = ProductNames.Onions },
                        new ItemEntity { Name = ProductNames.Yoghurt, Next = ProductNames.Onions}
                    }
                },
                new ListEntity
                {
                    ShopName = ShopNames.Sainsburys,
                    Name = "Berrys List",
                    Items = new List<IItemEntity>
                    {
                        new ItemEntity { Name = ProductNames.Crisps, Next = ProductNames.QuornNuggets },
                        new ItemEntity { Name = ProductNames.QuornNuggets },
                        new ItemEntity { Name = ProductNames.Sausages, Next = ProductNames.Crisps, IsFirst = true }
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
            ListEntity list = _lists.FirstOrDefault(list => list.Name == name);
            list.Items = list.Items.Sort();
            return Task.FromResult(list as IListEntity);
        }
    }
}
