using FinanceTracker.Models;

namespace FinanceTracker.Interfaces
{
    public interface ICommand
    {
        public void Execute(Account account);
    }
}
