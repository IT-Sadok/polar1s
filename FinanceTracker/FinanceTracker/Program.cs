using FinanceTracker.Commands;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;

Account account = new Account();

string? readResult;
string menuSelection = "";

do
{
    DisplayMainMenu();

    readResult = Console.ReadLine();
    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }

    switch (menuSelection)
    {
        case "1":
            ICommand getIncome = new GetIncome();
            getIncome.Execute(account);
            break;
        case "2":
            ICommand getExpence = new GetExpence();
            getExpence.Execute(account);
            break;
        case "3":
            ICommand getBalance = new GetBalance();
            getBalance.Execute(account);
            break;
        default:
            break;
    }

} while (menuSelection != "exit");

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