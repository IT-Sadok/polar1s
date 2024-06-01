
namespace FinanceTracker.Commands
{
    public class GetWeekStatistics : GetStatisticsBase
    {
        protected override DateTime GetStartDate()
        {
            return DateTime.UtcNow.AddDays(-7);
        }
    }
}
