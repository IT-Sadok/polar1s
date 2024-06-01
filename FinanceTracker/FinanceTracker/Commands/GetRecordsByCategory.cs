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
            decimal total = account.Operations.Where(operation => operation.CategoryId == categoryId).Sum(operation => operation.Amount);
            Console.WriteLine($"Total: {total}");
            Console.ReadLine();
        }
    }
}
