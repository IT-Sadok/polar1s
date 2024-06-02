using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using System.Globalization;

namespace FinanceTracker.Commands
{
    public class GetBalanceCommand : ICommand<decimal>
    {
        public decimal Execute(Account account)
        {
            return account.Balance;
        }
    }
}
