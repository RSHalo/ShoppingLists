namespace ShoppingList.Web.Models.Shops
{
    public class ExistingProductsModel
    {
        public string ShopName { get; set; }

        public IList<ProductModel> Products { get; set; }
    }
}
