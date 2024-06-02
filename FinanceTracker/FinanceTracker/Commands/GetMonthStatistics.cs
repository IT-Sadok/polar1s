
namespace FinanceTracker.Commands
{
    public class GetMonthStatistics : GetStatisticsBase
    {
        protected override DateTime GetStartDate()
        {
            return DateTime.UtcNow.AddMonths(-1);
        }
    }
}
