using FinanceTracker.Enums;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

namespace FinanceTracker.Commands
{
    public abstract class AddOperationCommandBase : ICommand<bool>
    {
        protected OperationType _type;
        public bool Execute(Account account)
        {
            UserInputReader reader = new UserInputReader();
            Guid id = Guid.NewGuid();
            int categoryId = reader.GetCategory(_type);
            decimal amount = reader.GetAmount();
            string description = reader.GetDescription();
            DateTime date = DateTime.UtcNow;
            Operation operation = new Operation(id, _type, categoryId, amount, description, date);
            account.UpdateBalance(operation);

            return (operation != null) ? true : false; 
        }

        public AddOperationCommandBase(OperationType type)
        {
            _type = type;
        }
    }
}
