using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using System.Globalization;

namespace FinanceTracker.Commands
{
    public class GetBalanceCommand : ICommand
    {
        public void Execute(Account account)
        {
            Console.WriteLine($"Balance: {account.Balance} UAH");

            Console.ReadLine();
        }
    }
}
