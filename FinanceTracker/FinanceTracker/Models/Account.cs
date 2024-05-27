using FinanceTracker.Enums;

namespace FinanceTracker.Models
{
    public class Account
    {
        public List<Operation> Operations;
        public decimal Balance { get; private set; }

        public Account(List<Operation> operations)
        {
            Operations = operations;

            foreach (var operation in Operations)
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
