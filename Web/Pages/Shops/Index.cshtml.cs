using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Data.Shops;
using ShoppingList.Web.Helper;
using ShoppingList.Web.Models.Shops;

namespace ShoppingList.Web.Pages.Shops
{
    public class IndexModel : BasePageModel
    {
        private readonly IShopRepository _shopRepository;

        public IndexModel(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;

            Title = "All Shops";
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
