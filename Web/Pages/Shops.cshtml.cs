using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;

namespace ShoppingList.Web.Pages
{
    public class ShopsModel : PageModel
    {
        private readonly IShopRepository _shopRepository;

        public ShopsModel(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public Dictionary<IShopEntity, IList<IProductEntity>> ShopsWithProducts { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ShopsWithProducts = new Dictionary<IShopEntity, IList<IProductEntity>>();

            IList<IShopEntity> shops = await _shopRepository.AllShopsAsync();
            foreach (IShopEntity shop in shops)
            {
                IList<IProductEntity> products = await _shopRepository.AllProductsForShop(shop.Name);
                ShopsWithProducts.Add(shop, products);
            }

            return Page();
        }
    }
}
