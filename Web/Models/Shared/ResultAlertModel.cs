namespace ShoppingList.Web.Models.Shared
{
    /// <summary>
    /// The model for an alert that displays the result of an action.
    /// </summary>
    public class ResultAlertModel
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public static ResultAlertModel Success(string message)
        {
            return new ResultAlertModel
            {
                IsSuccess = true,
                Message = message
            };
        }

        public static ResultAlertModel Fail(string message)
        {
            return new ResultAlertModel
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
