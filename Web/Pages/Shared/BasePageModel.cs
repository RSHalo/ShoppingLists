using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingList.Web.Pages
{
    public abstract class BasePageModel : PageModel
    {
        [ViewData]
        public string Title { get; protected set; }
    }
}
