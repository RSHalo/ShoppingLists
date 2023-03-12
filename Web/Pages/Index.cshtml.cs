using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingList.Web.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("Lists/All");
        }
    }
}