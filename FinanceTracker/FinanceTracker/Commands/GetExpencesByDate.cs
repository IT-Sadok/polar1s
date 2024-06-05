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

            Dictionary<DateTime, decimal> expenses = account.Operations
                .Where(operation => operation.Type == OperationType.Expense)
                .GroupBy(operation => operation.Date.Date)
                .ToDictionary(
                operations => operations.Key,
                operations => operations.Sum(operation => operation.Amount)
                );


            //return account.Operations
            //    .Where(operation => operation.Type == OperationType.Expense && operation.Date.Date == date.Date)
            //    .Sum(operation => operation.Amount);


            return expenses.TryGetValue(date, out var total) ? total : default;
        }
    }
}
