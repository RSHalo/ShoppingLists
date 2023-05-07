using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Core.Products;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;
using ShoppingList.Web.Helper;
using ShoppingList.Web.Models.Shops;

namespace ShoppingList.Web.Pages.Shops
{
    public class IndexModel : BasePageModel
    {
        private readonly IShopRepository _shopRepository;
        private readonly IProductMaintainer _productMaintainer;

        public IndexModel(IShopRepository shopRepository, IProductMaintainer productMaintainer)
        {
            _shopRepository = shopRepository;
            _productMaintainer = productMaintainer;
            Title = "All Shops";
        }

        public IList<ShopModel> Shops { get; set; } = new List<ShopModel>();

        public async Task<IActionResult> OnGet()
        {
            IList<IShopEntity> shops = await _shopRepository.AllShopsAsync();
            Shops = shops.Select(ModelMapper.ToShopModel).ToList();

            return Page();
        }

        public async Task<IActionResult> OnGetAllProductsAsync(string shopName)
        {
            IList<IProductEntity> products = await _shopRepository.AllProductsForShop(shopName);
            IList<ProductModel> productModels = products.Select(ModelMapper.ToProductModel).ToList();

            ExistingProductsModel model = new ExistingProductsModel { ShopName = shopName, Products = productModels };
            return Partial("_ExistingProducts", model);
        }

        public async Task<IActionResult> OnPostDeleteProductAsync(string shopName, string productName)
        {
            bool success = await _productMaintainer.RemoveProductAsync(shopName, productName);
            if (success)
            {
                return new OkResult();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
