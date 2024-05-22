namespace FinanceTracker.Models
{
    public class Account
    {
        public decimal Balance
        {
            get
            {
                return Incomes - Expences;
            }
        }
        public decimal Incomes { get; set; }
        public decimal Expences { get; set; }

        public Account()
        {
            Incomes = 0.0m;
            Expences = 0.0m;
        }

        public void AddIncome(decimal amount)
        {
            Incomes += amount;
        }

        public void AddExpence(decimal amount)
        {
            Expences += amount;
        }
    }
}
