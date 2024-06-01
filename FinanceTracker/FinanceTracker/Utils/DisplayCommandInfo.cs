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

        public void ShowWeekStatistics(decimal statistics)
        {
            Console.WriteLine($"Your week statistic: {statistics} UAH");
            Console.ReadLine();
        }

        public void ShowMonthStatistics(decimal statistics)
        {
            Console.WriteLine($"Your month statistic: {statistics} UAH");
            Console.ReadLine();
        }
    }
}
