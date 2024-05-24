using FinanceTracker.Enums;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

namespace FinanceTracker.Commands
{
    public class AddExpenceCommand : Command
    {
        public AddExpenceCommand() : base(OperationType.Expence) { }
    }
}
