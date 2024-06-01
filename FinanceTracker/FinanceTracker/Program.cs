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

    DisplayCommandInfo? display = new DisplayCommandInfo();
    switch (menuSelection)
    {
        case "1":
            ICommand<Operation> addIncomeCommand = new AddIncomeCommand();
            account.UpdateBalance(addIncomeCommand.Execute(account));
            break;
        case "2":
            ICommand<Operation> addExpenceCommand = new AddExpenceCommand();
            account.UpdateBalance(addExpenceCommand.Execute(account));
            break;
        case "3":
            ICommand<decimal> getBalanceCommand = new GetBalanceCommand();
            display.ShowBalance(getBalanceCommand.Execute(account));
            break;
        case "4":
            ICommand<decimal> getRecordsByCategoryCommand = new GetRecordsByCategory();
            display.ShowCategoryRecords(getRecordsByCategoryCommand.Execute(account));
            break;
        case "5":
            ICommand<decimal> getWeekStatiscticsCommand = new GetWeekStatistics();
            display.ShowWeekStatistics(getWeekStatiscticsCommand.Execute(account));
            break;
        case "6":
            ICommand<decimal> getMonthStatiscticsCommand = new GetMonthStatistics();
            display.ShowMonthStatistics(getMonthStatiscticsCommand.Execute(account));
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
    Console.WriteLine("4. Get records by specific category");
    Console.WriteLine("5. Show WEEK statistic");
    Console.WriteLine("6. Show MONTH statistic");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");
}