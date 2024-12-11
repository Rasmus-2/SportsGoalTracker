namespace SportsGoalApp.Utilities
{
    public class PercentageCalculator : ICalculatePercentage
    {
        public float CalculatePercentage(int? totalNumber, int? successfulNumber)
        {
            if (totalNumber.HasValue && successfulNumber.HasValue && totalNumber.Value > 0)
            {
                return (float)successfulNumber.Value / totalNumber.Value * 100;
            }
            return 0;
        }
    }
}
