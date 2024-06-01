namespace FinanceTracker.Utils
{
    public class DisplayCommandInfo
    {
        public void ShowBalance(decimal balance)
        {
            Console.WriteLine($"Balance: {balance} UAH");
            Console.ReadLine();
        }

        public void ShowCategoryRecords(decimal total)
        {
            Console.WriteLine($"Total: {total}");
            Console.ReadLine();
        }
    }
}
