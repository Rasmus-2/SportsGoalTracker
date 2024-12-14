
namespace SportsGoalApp.Utilities
{
    public interface IAvailableDateChecker
    {
        bool CheckForbiddenDate(DateOnly oldGoalStartDate, DateOnly oldGoalEndDate, DateOnly newGoalStartDate, DateOnly newGoalEndDate);
    }
}