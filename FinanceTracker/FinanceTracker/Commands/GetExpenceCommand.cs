using FinanceTracker.Enums;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;

namespace FinanceTracker.Commands
{
    public class GetExpenceCommand : ICommand
    {
        public void Execute(Account account)
        {
            UserInputReader reader = new UserInputReader();
            decimal amount = reader.GetAmount();
            string description = reader.GetDescription();
            Operation operation = new Operation(OperationType.Expence, amount, description);
            account.Operations.Add(operation);

            Console.ReadLine();
        }
    }
}
