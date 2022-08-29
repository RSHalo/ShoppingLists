namespace ShoppingList
{
    internal class NewItemArgs
    {
        public string NewItem { get; }
        public bool BeforeExisting { get; }
        public string ExistingItem { get; }

        public NewItemArgs(string inputLine)
        {
            string[] inputParts = inputLine.Split(" ");
            NewItem = inputParts[0];
            BeforeExisting = inputParts[1] == "b";
            ExistingItem = inputParts[2];
        }
    }
}
