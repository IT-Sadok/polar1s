using FinanceTracker.Commands;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;

Account account = new Account();

string? userInput;
string menuSelection = "";

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
            ICommand getIncome = new AddIncomeCommand();
            getIncome.Execute(account);
            break;
        case "2":
            ICommand getExpence = new AddExpenceCommand();
            getExpence.Execute(account);
            break;
        case "3":
            ICommand getBalance = new GetBalanceCommand();
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