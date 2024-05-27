using FinanceTracker.Commands;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

const string FILEPATH = "db.json";

string? userInput;
string menuSelection = "";

List<Operation> operations = JsonFileManager.ReadFromJson(FILEPATH);
Account account = new Account(operations);

do
{
    DisplayMainMenu();

    userInput = Console.ReadLine();
    if (userInput != null)
    {
        menuSelection = userInput.ToLower();
    }

    ICommand? command = null;
    switch (menuSelection)
    {
        case "1":
            command = new AddIncomeCommand();
            command.Execute(account);
            break;
        case "2":
            command = new AddExpenceCommand();
            command.Execute(account);
            break;
        case "3":
            command = new GetBalanceCommand();
            command.Execute(account);
            break;
        default:
            break;
    }

} while (menuSelection != "exit");

JsonFileManager.WriteToJson(account, FILEPATH);

void DisplayMainMenu()
{
    Console.Clear();

    Console.WriteLine("Welcome to FinanceTracker!\nChoose an option:");
    Console.WriteLine("1. Add income");
    Console.WriteLine("2. Add expence");
    Console.WriteLine("3. Show balance");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");
}