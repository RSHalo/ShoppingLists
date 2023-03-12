using ShoppingList.Data.Lists;

namespace ShoppingList.Web.Models.Lists
{
    public class ListModel
    {
        public string ShopName { get; set; }

        public string Name { get; set; }

        public IList<ItemModel> Items { get; set; }
    }
}
