namespace ShoppingList.Web.Models.Lists.ShoppingMode
{
    public class PickedItemsModel : ItemsModel
    {
        public override string PickedStatusCssClass => PickedCssClass;

        public override string NoItemsMessage => "No items have been picked.";

        public override string PickButtonText => "Unpick";
    }
}
