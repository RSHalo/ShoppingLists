namespace ShoppingList.Web.Models.Lists.ShoppingMode
{
    public class UnpickedItemsModel : ItemsModel
    {
        public override string PickedStatusCssClass => UnpickedCssClass;

        public override string NoItemsMessage => "All items have been picked!";
    }
}
