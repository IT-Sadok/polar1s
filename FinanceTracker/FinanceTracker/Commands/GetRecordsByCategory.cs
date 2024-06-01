using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

namespace FinanceTracker.Commands
{
    public class GetRecordsByCategory : ICommand
    {
        public void Execute(Account account)
        {
            UserInputReader reader = new UserInputReader();
            int categoryId = reader.GetCategory(account);
            Console.WriteLine($"Total: {account.GetRecords(categoryId)}");
            Console.ReadLine();
        }
    }
}
