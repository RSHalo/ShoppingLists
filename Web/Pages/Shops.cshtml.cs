using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;
using ShoppingList.Web.Helper;
using ShoppingList.Web.Models.Shops;

namespace ShoppingList.Web.Pages
{
    public class ShopsModel : PageModel
    {
        private readonly IShopRepository _shopRepository;

        public ShopsModel(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public IList<ShopModel> Shops { get; set; } = new List<ShopModel>();

        public async Task<IActionResult> OnGet()
        {
            IList<IShopEntity> shops = await _shopRepository.AllShopsAsync();
            Shops = shops.Select(ModelMapper.ToModel).ToList();

            return Page();
        }
    }
}
