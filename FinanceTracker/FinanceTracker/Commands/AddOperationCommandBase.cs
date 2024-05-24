using FinanceTracker.Enums;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

namespace FinanceTracker.Commands
{
    public abstract class AddOperationCommandBase : ICommand
    {
        protected OperationType _type;
        public void Execute(Account account)
        {
            UserInputReader reader = new UserInputReader();
            decimal amount = reader.GetAmount();
            string description = reader.GetDescription();
            Operation operation = new Operation(_type, amount, description);
            account.Operations.Add(operation);
            account.UpdateBalance(operation);
        }

        public AddOperationCommandBase(OperationType type)
        {
            _type = type;
        }
    }
}
