using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Data.Lists;
using ShoppingList.Web.Helper;
using ShoppingList.Web.Models.Lists;

namespace ShoppingList.Web.Pages.Lists
{
    public class IndexModel : PageModel
    {
        private readonly IListRepository _listRepository;

        public IndexModel(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public ListModel List { get; set; }

        public async Task<IActionResult> OnGet(string listName)
        {
            IListEntity entity = await _listRepository.FindListAsync(listName);
            List = entity.ToListModel();
            return Page();
        }

        public async Task<IActionResult> OnGetItemsAsync(string listName)
        {
            IListEntity entity = await _listRepository.FindListAsync(listName);
            List = entity.ToListModel();
            return Partial("_Items", this);
        }

        public async Task<IActionResult> OnPostToggleItemAsync(string listName, string itemName, bool toggleToOn)
        {
            if (toggleToOn)
            {
                await _listRepository.AddItemAsync(listName, itemName);
            }
            else
            {
                await _listRepository.RemoveItemAsync(listName, itemName);
            }

            return new OkResult();
        }
    }
}
