using FinanceTracker.Enums;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.Commands
{
    public class GetIncomeCommand : ICommand
    {
        public void Execute(Account account)
        {
            UserInputReader reader = new UserInputReader();
            decimal amount = reader.GetAmount();
            string description = reader.GetDescription();
            Operation operation = new Operation(OperationType.Income, amount, description);
            account.Operations.Add(operation);

            Console.ReadLine();
        }
    }
}
