using SportsGoalApp.Utilities;

namespace SportsGoalAppTests
{
    public class PracticeLogTests
    {

        private readonly ICalculatePercentage _percentageCalculator;

        public PracticeLogTests()
        {
            _percentageCalculator = new PercentageCalculator();
        }

        [Theory]
        [InlineData(100, 50, 50)]
        [InlineData(200, 100, 50)]
        [InlineData(100, 100, 100)]
        public void CalculatePercantage_ValidInputs_ReturnExpectedPercentage(int totalNumber, int successfulNumber, float expected)
        {
            // Arrange

            // Act
            float actual = _percentageCalculator.CalculatePercentage(totalNumber, successfulNumber);

            // Assert 
            Assert.Equal(expected, actual, 2);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 0)]
        public void CalculatePercentage_ZeroOrNoSuccessful_ReturnsZeroPercentage(int totalNumber, int successfulNumber)
        {
            // Arrange
            float expected = 0;

            // Act
            float actual = _percentageCalculator.CalculatePercentage(totalNumber, successfulNumber);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(100, null)]
        [InlineData(null, 50)]
        public void CalculatePercentage_NullInputs_ReturnsZeroPercentage(int? totalNumber, int? successfulNumber)
        {
            // Arrange
            float expected = 0;

            // Act
            float actual = _percentageCalculator.CalculatePercentage(totalNumber, successfulNumber);


            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(75, 50, 66.66667)]
        [InlineData(150, 100, 66.66667)]
        [InlineData(90, 60, 66.66667)]
        [InlineData(200, 133, 66.5)]
        [InlineData(75, 25, 33.33333)]
        public void CalculatePercentage_DifferentValidInputs_ReturnsExpectedPercentageWithDecimals(int totalNumber, int successfulNumber, float expected)
        {
            // Arrange

            // Act
            float actual = _percentageCalculator.CalculatePercentage(totalNumber, successfulNumber);

            // Assert
            float tolerance = 0.00001f;
            Assert.InRange(actual, expected - tolerance, expected + tolerance);
        }
    }
}