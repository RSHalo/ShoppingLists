using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Web.Helper;
using ShoppingList.Web.Models.Shared;

namespace ShoppingList.Web.Pages
{
    public abstract class BasePageModel : PageModel
    {
        [ViewData]
        public string Title { get; protected set; }

        protected void AddSuccessAlert(string message)
        {
            AddAlert(ResultAlertModel.Success(message));
        }

        protected void AddFailureAlert(string message)
        {
            AddAlert(ResultAlertModel.Fail(message));
        }

        private void AddAlert(ResultAlertModel model)
        {
            TempData.Set(TempDataExtensions.ResultAlertKey, model);
        }
    }
}
