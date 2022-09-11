namespace ShoppingList.Web.Models
{
    public class RegisterItemModel
    {
        public string ShopName { get; set; }

        public string NewProductName { get; set; }

        public string NextProductName { get; set; }

        public bool IsFirst { get; set; }
    }
}
