using FinanceTracker.Commands;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

string? userInput;
string menuSelection = "";
string filePath = "db.json";

List<Operation> operations = JsonFileManager.ReadFromJson(filePath);
Account account = new Account(operations);

do
{
    DisplayMainMenu();

    userInput = Console.ReadLine();
    if (userInput != null)
    {
        menuSelection = userInput.ToLower();
    }

    switch (menuSelection)
    {
        case "1":
            ICommand addIncome = new AddIncomeCommand();
            addIncome.Execute(account);
            break;
        case "2":
            ICommand addExpence = new AddExpenceCommand();
            addExpence.Execute(account);
            break;
        case "3":
            ICommand getBalance = new GetBalanceCommand();
            getBalance.Execute(account);
            break;
        default:
            break;
    }

} while (menuSelection != "exit");

JsonFileManager.WriteToJson(account.Operations, filePath);

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