using FinanceTracker.Enums;

namespace FinanceTracker.Commands
{
    public class AddExpenceCommand : AddOperationCommandBase
    {
        public AddExpenceCommand() : base(OperationType.Expence) { }
    }
}
