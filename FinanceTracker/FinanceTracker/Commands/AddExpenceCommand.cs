using FinanceTracker.Enums;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

namespace FinanceTracker.Commands
{
    public class AddExpenceCommand : AddOperationCommandBase
    {
        public AddExpenceCommand() : base(OperationType.Expence) { }
    }
}
