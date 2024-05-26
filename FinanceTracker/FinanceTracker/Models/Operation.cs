﻿using FinanceTracker.Enums;
using System.Text.Json.Serialization;

namespace FinanceTracker.Models
{
    public class Operation
    {
        [JsonPropertyName("id")]
        public Guid ID { get; set; }

        [JsonPropertyName("type")]
        public OperationType Type { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        public Operation(Guid id, OperationType type, decimal amount, string description, string date)
        {
            ID = id;
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
