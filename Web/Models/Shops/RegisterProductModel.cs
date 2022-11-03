namespace ShoppingList.Web.Models.Shops
{
    public class RegisterProductModel
    {
        public const string DefaultModalId = "registerProductModal";

        public string ModalId { get; set; } = DefaultModalId;

        public string NewNameInputId => $"{ModalId}-newName";

        public string AddToStartCheckboxId => $"{ModalId}-addToStart";

        public string SubmitButtonId => $"{ModalId}-submit";

        public string ShopName { get; set; }

        public string NewProductName { get; set; }

        public string NextProductName { get; set; }
    }
}
