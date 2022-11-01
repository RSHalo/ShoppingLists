using Microsoft.AspNetCore.Mvc;
using ShoppingList.Data.Helper;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;
using ShoppingList.Web.Helper;
using ShoppingList.Web.Models;

namespace ShoppingList.Web.Controllers
{
    public class ShopsController : Controller
    {
        private readonly IShopRepository _shopRepository;
        public const string ControllerName = "Shops";

        public ShopsController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<IActionResult> ProductOptions(string shopName)
        {
            IList<IProductEntity> products = await _shopRepository.AllProductsForShop(shopName);
            IList<ProductModel> productModels = products.Select(ModelMapper.ToModel).ToList();

            ExistingItemOptionsModel model = new ExistingItemOptionsModel { Products = productModels };
            return PartialView("_ExistingItemOptions", model);
        }
    }
}
