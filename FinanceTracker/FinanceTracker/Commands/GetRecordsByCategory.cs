using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

namespace FinanceTracker.Commands
{
    public class GetRecordsByCategory : ICommand<decimal>
    {
        public decimal Execute(Account account)
        {
            UserInputReader reader = new UserInputReader();
            int categoryId = reader.GetCategory(account);
            return account.Operations.Where(operation => operation.CategoryId == categoryId).Sum(operation => operation.Amount);
        }
    }
}
