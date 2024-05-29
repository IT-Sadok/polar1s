using FinanceTracker.Enums;

namespace FinanceTracker.Models
{
    public class Operation
    {
        public Guid Id { get; set; }

        public OperationType Type { get; set; }

        public int CategoryId { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public Operation(Guid id, OperationType type, int categoryId, decimal amount, string description, DateTime date)
        {
            Id = id;
            Type = type;
            CategoryId = categoryId;
            Amount = amount;
            Description = description;
            Date = date;
        }
    }
}
