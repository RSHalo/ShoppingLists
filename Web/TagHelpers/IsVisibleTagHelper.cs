using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ShoppingList.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = AttributeName)]
    public class IsVisibleTagHelper : TagHelper
    {
        private const string AttributeName = "is-visible";

        [HtmlAttributeName(AttributeName)]
        public bool Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Condition == false)
            {
                output.SuppressOutput();
            }
        }
    }
}
