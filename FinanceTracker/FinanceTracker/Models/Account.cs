using FinanceTracker.Enums;

namespace FinanceTracker.Models
{
    public class Account
    {
        public List<Operation> Operations = new();
        public decimal Balance { get; private set; }

        public void UpdateBalance(Operation operation)
        {
            if (operation.Type is OperationType.Income)
            {
                Balance += operation.Amount;
            }
            else
            {
                Balance -= operation.Amount;
            }
        }
    }
}
