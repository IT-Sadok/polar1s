using FinanceTracker.Models;

namespace FinanceTracker.Interfaces
{
    public interface ICommand<T>
    {
        public T Execute(Account account);
    }
}
