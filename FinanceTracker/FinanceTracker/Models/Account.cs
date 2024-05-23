namespace FinanceTracker.Models
{
    public class Account
    {
        public List<Operation> Operations = new();
        public decimal Balance
        {
            get
            {
                decimal delta = 0.0m;
                foreach (var operation in Operations)
                {
                    if (operation.IsIncome())
                    {
                        delta += operation.Amount;
                    }
                    else
                    {
                        delta -= operation.Amount;
                    }
                }

                return delta;
            }
        }
    }
}
