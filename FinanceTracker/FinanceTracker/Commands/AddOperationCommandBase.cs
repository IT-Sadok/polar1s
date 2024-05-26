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
            Guid id = Guid.NewGuid();
            decimal amount = reader.GetAmount();
            string description = reader.GetDescription();
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            Operation operation = new Operation(id, _type, amount, description, date);
            account.Operations.Add(operation);
            account.UpdateBalance(operation);
        }

        public AddOperationCommandBase(OperationType type)
        {
            _type = type;
        }
    }
}
