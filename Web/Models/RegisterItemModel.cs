namespace ShoppingList.Web.Models
{
    public class RegisterItemModel
    {
        public const string DefaultModalId = "registerItemModal";

        public string ModalId { get; set; } = DefaultModalId;

        public string ShopName { get; set; }

        public string NewProductName { get; set; }

        public string NextProductName { get; set; }

        public bool IsFirst { get; set; }
    }
}
