namespace FinanceTracker.Utils
{
    public class DisplayCommandInfo
    {
        public void ShowOperationResult(bool success)
        {
            if (success)
            {
                Console.WriteLine("Done!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Oops, something went wrong:(");
                Console.ReadLine();
            }
        }
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

        public void ShowExpensesByDate(decimal total)
        {
            Console.WriteLine($"Total expenses for that date: {total} UAH");
            Console.ReadLine();
        }
    }
}
