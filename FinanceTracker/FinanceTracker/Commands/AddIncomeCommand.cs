using FinanceTracker.Enums;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

namespace FinanceTracker.Commands
{
    public class AddIncomeCommand : Command
    {

        public AddIncomeCommand() : base(OperationType.Income) { }
    }
}
