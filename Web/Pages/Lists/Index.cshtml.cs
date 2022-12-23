using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Data.Lists;

namespace ShoppingList.Web.Pages.Lists
{
    public class IndexModel : PageModel
    {
        private readonly IListRepository _listRepository;

        public IndexModel(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public IListEntity List { get; set; }

        public async Task<IActionResult> OnGet(string listName)
        {
            List = await _listRepository.FindListAsync(listName);

            return Page();
        }

        public async Task<IActionResult> OnGetTryItAsync(string listName)
        {
            List = await _listRepository.FindListAsync(listName);

            return Partial("_Items", this);
        }
    }
}
