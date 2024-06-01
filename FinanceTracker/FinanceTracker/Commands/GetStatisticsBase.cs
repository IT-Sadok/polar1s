using FinanceTracker.Enums;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.Commands
{
    public abstract class GetStatisticsBase : ICommand<decimal>
    {
        protected abstract DateTime GetStartDate();
        public decimal Execute(Account account)
        {
            DateTime startDate = GetStartDate();
            DateTime endDate = DateTime.UtcNow;

            decimal total = 0.0m;

            var operations = account.Operations
                .Where(operation => operation.Date >= startDate && operation.Date <= endDate);

            foreach (var operation in operations)
            {
                if(operation.Type is OperationType.Income)
                {
                    total += operation.Amount;
                }
                else
                {
                    total -= operation.Amount;
                }
            }

            return total;
        }
    }
}
