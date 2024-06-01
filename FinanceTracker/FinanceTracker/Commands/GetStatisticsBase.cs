using FinanceTracker.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.Commands
{
    public abstract class GetStatisticsBase : ICommand<decimal>
    {
        public decimal Execute(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
