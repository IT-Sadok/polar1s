using FinanceTracker.Enums;

namespace FinanceTracker.Models
{
    public class Operation
    {
        OperationType Type { get; set; }
        public Guid ID { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public Operation(OperationType operationType, decimal amount, string description)
        {
            ID = Guid.NewGuid();
            Type = operationType;
            Amount = amount;
            Description = description;
            DateTime = DateTime.Now;
        }

        public bool IsIncome()
        {
            return (Type == OperationType.Income) ? true: false;
        }
    }
}
