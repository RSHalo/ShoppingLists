using Microsoft.AspNetCore.Mvc;
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
            IEnumerable<IListEntity> lists = await _listRepository.AllListsAsync();
            Lists = lists.ToList();

            IList<IShopEntity> shops = await _shopRepository.AllShopsAsync();
            Shops = shops.Select(ModelMapper.ToModel).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string listName, string shopName)
        {
            if (string.IsNullOrWhiteSpace(listName))
            {
                AddFailureAlert("No list name was provided.");
                return RedirectToPage();
            }

            IShopEntity shop = await _shopRepository.FindAsync(shopName);
            if (shop == null)
            {
                AddFailureAlert("The requested shop could not be found.");
                return RedirectToPage();
            }

            IListEntity existingList = await _listRepository.FindListAsync(listName);
            if (existingList != null)
            {
                AddFailureAlert($"A list called {listName} already exists.");
                return RedirectToPage();
            }

            bool success = await _listRepository.AddListAsync(listName, shopName);
            if (success)
            {
                AddSuccessAlert($"The {listName} list was added successfully.");
            }
            else
            {
                AddFailureAlert("Failed to add the list.");
            }

            return RedirectToPage();
        }
    }
}
