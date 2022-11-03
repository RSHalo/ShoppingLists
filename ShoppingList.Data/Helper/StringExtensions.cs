namespace ShoppingList.Data.Helper
{
    public static class StringExtensions
    {
        /// <summary>
        /// Whether the string has a non-null and non-whitespace value.
        /// </summary>
        public static bool HasValue(this string @string)
        {
            return string.IsNullOrWhiteSpace(@string) == false;
        }
    }
}
