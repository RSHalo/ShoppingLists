using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace ShoppingList.Web.Helper
{
    public static class TempDataExtensions
    {
        public const string ResultAlertKey = nameof(ResultAlertKey);

        /// <summary>
        /// Serializes an object to JSON and stores it in the <see cref="ITempDataDictionary"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="tempData">The TempData to store to.</param>
        /// <param name="key">The TempData key to use.</param>
        /// <param name="value">The object to serialize.</param>
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonSerializer.Serialize(value);
        }

        /// <summary>
        /// Retrieves a JSON string from an <see cref="ITempDataDictionary"/> and deserializes it into a <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize.</typeparam>
        /// <param name="tempData">The TempData.</param>
        /// <param name="key">The TempData key to use.</param>
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out object value);
            return value == null ? null : JsonSerializer.Deserialize<T>(value as string);
        }
    }
}
