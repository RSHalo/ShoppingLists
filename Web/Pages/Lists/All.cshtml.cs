using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Data.Lists;

namespace ShoppingList.Web.Pages.Lists
{
    public class AllModel : PageModel
    {
        private readonly IListRepository _listRepository;

        public AllModel(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public IList<IListEntity> Lists { get; set; }

        public async Task<IActionResult> OnGet()
        {
            IEnumerable<IListEntity> lists = await _listRepository.AllListsAsync();
            Lists = lists.ToList();

            return Page();
        }
    }
}
