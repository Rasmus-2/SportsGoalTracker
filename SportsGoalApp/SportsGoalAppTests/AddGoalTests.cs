using SportsGoalApp.Utilities;

namespace SportsGoalAppTests
{
    public class AddGoalTests
    {
        private readonly IAvailableDateChecker _availableDateChecker;

        public AddGoalTests()
        {
            _availableDateChecker = new AvailableDateChecker();
        }

        [Theory]
        [InlineData(1, 3, 5, 7, false)]
        [InlineData(1, 3, 4, 8, false)]
        [InlineData(1, 3, 4, 20, false)]
        [InlineData(10, 11, 5, 7, false)]
        [InlineData(10, 13, 1, 9, false)]
        [InlineData(21, 25, 26, 27, false)]
        public void CheckForbiddenDate_ValidInputs_ReturnFalse(int oldGoalStartDay, int oldGoalEndDay, int newGoalStartDay, int newGoalEndDay, bool expected)
        {
            // Arrange
            DateOnly oldGoalStartDate = new DateOnly(2024, 11, oldGoalStartDay);
            DateOnly oldGoalEndDate = new DateOnly(2024, 11, oldGoalEndDay);
            DateOnly newGoalStartDate = new DateOnly(2024, 11, newGoalStartDay);
            DateOnly newGoalEndDate = new DateOnly(2024, 11, newGoalEndDay);

            // Act
            bool actual = _availableDateChecker.CheckForbiddenDate(oldGoalStartDate, oldGoalEndDate, newGoalStartDate, newGoalEndDate);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 3, 3, 7, true)]
        [InlineData(1, 3, 2, 7, true)]
        [InlineData(1, 3, 1, 7, true)]
        [InlineData(3, 6, 1, 7, true)]
        [InlineData(10, 13, 5, 13, true)]
        [InlineData(10, 13, 10, 13, true)]
        public void CheckForbiddenDate_ValidInputs_ReturnTrue(int oldGoalStartDay, int oldGoalEndDay, int newGoalStartDay, int newGoalEndDay, bool expected)
        {
            // Arrange
            DateOnly oldGoalStartDate = new DateOnly(2024, 11, oldGoalStartDay);
            DateOnly oldGoalEndDate = new DateOnly(2024, 11, oldGoalEndDay);
            DateOnly newGoalStartDate = new DateOnly(2024, 11, newGoalStartDay);
            DateOnly newGoalEndDate = new DateOnly(2024, 11, newGoalEndDay);

            // Act
            bool actual = _availableDateChecker.CheckForbiddenDate(oldGoalStartDate, oldGoalEndDate, newGoalStartDate, newGoalEndDate);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
