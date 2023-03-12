namespace ShoppingList.Web.Models.Shared
{
    public class DialogueModel
    {
        public string Id { get; } = "i" + Guid.NewGuid().ToString("D");
    }

    public class DialogueModel<T> : DialogueModel
    {
        public DialogueModel(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
