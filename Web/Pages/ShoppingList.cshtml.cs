using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Data.Lists;

namespace ShoppingList.Web.Pages
{
    public class ShoppingListModel : PageModel
    {
        private readonly IListRepository _listRepository;

        public ShoppingListModel(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public IListEntity List { get; set; }

        public async Task<IActionResult> OnGet(string listName)
        {
            List = await _listRepository.FindListAsync(listName);

            return Page();
        }
    }
}
