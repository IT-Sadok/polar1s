using FinanceTracker.Enums;

namespace FinanceTracker.Commands
{
    public class AddIncomeCommand : AddOperationCommandBase
    {
        public AddIncomeCommand() : base(OperationType.Income) { }
    }
}
