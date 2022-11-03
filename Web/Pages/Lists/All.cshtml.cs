using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Data.Lists;
using ShoppingList.Data.Shops;
using ShoppingList.Web.Helper;
using ShoppingList.Web.Models.Shops;

namespace ShoppingList.Web.Pages.Lists
{
    public class AllModel : BasePageModel
    {
        private readonly IListRepository _listRepository;
        private readonly IShopRepository _shopRepository;

        public AllModel(IListRepository listRepository, IShopRepository shopRepository)
        {
            _listRepository = listRepository;
            _shopRepository = shopRepository;

            Title = "All Lists";
        }

        public IList<IListEntity> Lists { get; set; }

        public IList<ShopModel> Shops { get; set; }

        public async Task<IActionResult> OnGet()
        {
            return await PageAsync();
        }

        public async Task<IActionResult> OnPostAsync(string listName, string shopName)
        {
            if (string.IsNullOrWhiteSpace(listName))
            {
                // bad. Show message.
                return await PageAsync();
            }

            IShopEntity shop = await _shopRepository.FindAsync(shopName);
            if (shop == null)
            {
                // bad. Show message.
                return await PageAsync();
            }

            IListEntity existingList = await _listRepository.FindListAsync(listName);
            if (existingList != null)
            {
                // bad. Show message.
                return await PageAsync();
            }

            bool success = await _listRepository.AddListAsync(listName, shopName);
            if (success)
            {
                // success message.
            }
            else
            {
                // fail message.
            }

            return RedirectToPage();
        }

        private async Task<PageResult> PageAsync()
        {
            IEnumerable<IListEntity> lists = await _listRepository.AllListsAsync();
            Lists = lists.ToList();

            IList<IShopEntity> shops = await _shopRepository.AllShopsAsync();
            Shops = shops.Select(ModelMapper.ToModel).ToList();

            return Page();
        }
    }
}
