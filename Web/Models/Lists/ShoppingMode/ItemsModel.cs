namespace ShoppingList.Web.Models.Lists.ShoppingMode
{
    public abstract class ItemsModel
    {
        protected const string PickedCssClass = "picked";
        protected const string UnpickedCssClass = "unpicked";

        public IList<ItemModel> Items { get; set; } = new List<ItemModel>();

        public abstract string PickedStatusCssClass { get; }

        public abstract string NoItemsMessage { get; }
    }
}
