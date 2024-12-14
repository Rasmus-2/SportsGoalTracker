namespace SportsGoalApp.Utilities
{
    public class AvailableDateChecker : IAvailableDateChecker
    {
        public bool CheckForbiddenDate(DateOnly oldGoalStartDate, DateOnly oldGoalEndDate, DateOnly newGoalStartDate, DateOnly newGoalEndDate)
        {
            if (newGoalStartDate <= oldGoalEndDate && newGoalEndDate >= oldGoalStartDate)
            {
                return true;
            }
            return false;
        }
    }
}