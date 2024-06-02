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

            var operations = account.Operations
                .Where(operation => operation.Date >= startDate && operation.Date <= endDate);

            decimal total = operations.Aggregate(0.0m, (accum, operation) 
                => operation.Type is OperationType.Income ? accum + operation.Amount : accum - operation.Amount);

            return total;
        }
    }
}
