using ShoppingList;

List<string> items = new List<string>
{
    "apples",
    "bananas",
    "yoghurt",
    "onions"
};

foreach (string item in items)
{
    Console.WriteLine(item);
}

string input = Console.ReadLine();
NewItemArgs newItemArgs = new NewItemArgs(input);

int index = items.IndexOf(newItemArgs.ExistingItem);
int newIndex;
if (newItemArgs.BeforeExisting)
{
    newIndex = index;
}
else
{
    newIndex = index + 1;
}

items.Insert(newIndex, newItemArgs.NewItem);

foreach (string item in items)
{
    Console.WriteLine(item);
}