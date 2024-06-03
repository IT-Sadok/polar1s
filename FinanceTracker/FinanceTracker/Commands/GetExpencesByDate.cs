using FinanceTracker.Enums;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

namespace FinanceTracker.Commands
{
    public class GetExpencesByDate : ICommand<decimal>
    {
        public decimal Execute(Account account)
        {
            UserInputReader reader = new UserInputReader();
            DateTime date = reader.GetDate();

            var expenses = account.Operations
                .Where(operation => operation.Type == OperationType.Expense)
                .GroupBy(operation => operation.Date.Date)
                .FirstOrDefault(group => group.Key == date.Date);

            
            return expenses?.Aggregate(0.0m, (accum, operation) => accum + operation.Amount) ?? 0.0m;
        }
    }
}
