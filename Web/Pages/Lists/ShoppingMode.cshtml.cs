using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Data.Lists;
using ShoppingList.Web.Helper;
using ShoppingList.Web.Models.Lists.ShoppingMode;

namespace ShoppingList.Web.Pages.Lists
{
    public class ShoppingModeModel : PageModel
    {
        private readonly IListRepository _listRepository;

        public ShoppingModeModel(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public IListEntity List { get; set; }

        public ItemsModel PickedItems { get; set; } = new PickedItemsModel();

        public ItemsModel UnpickedItems { get; set; } = new UnpickedItemsModel();

        public async Task<IActionResult> OnGet(string listName)
        {
            List = await _listRepository.FindListAsync(listName);
            BuildItemsModel(PickedItems, item => item.IsPicked);
            BuildItemsModel(UnpickedItems, item => item.IsPicked == false);

            return Page();
        }

        public async Task<IActionResult> OnPostTogglePickStatusAsync(string listName, string itemName, bool markAsPicked)
        {
            if (markAsPicked)
            {
                await _listRepository.PickItemAsync(listName, itemName);
            }
            else
            {
                await _listRepository.UnpickItemAsync(listName, itemName);
            }

            return new OkResult();
        }

        private void BuildItemsModel(ItemsModel model, Func<IItemEntity, bool> itemsSelector)
        {
            foreach (IItemEntity item in List.Items)
            {
                if (item.IsOn)
                {
                    if (itemsSelector(item))
                    {
                        model.Items.Add(item.ToItemModel());
                    }
                }
            }
        }
    }
}
