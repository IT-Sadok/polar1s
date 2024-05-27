using FinanceTracker.Enums;
using System.Text.Json.Serialization;

namespace FinanceTracker.Models
{
    public class Operation
    {
        public Guid Id { get; set; }

        public OperationType Type { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public Operation(Guid id, OperationType type, decimal amount, string description, string date)
        {
            Id = id;
            Type = type;
            Amount = amount;
            Description = description;
            Date = date;
        }

        public bool IsIncome()
        {
            return (Type == OperationType.Income) ? true: false;
        }
    }
}
