using SportsGoalApp.Models;
using Xunit;

namespace SportsGoalAppTests
{
    public class PracticeLogTests
    {

        [Theory]
        [InlineData(100, 50, 50)]
        [InlineData(200, 100, 50)]
        [InlineData(100, 100, 100)]
        public void CalculatePercantage_ValidInputs_ReturnExpectedPercentage(int totalNumber, int successfulNumber, float expected)
        {
            // Arrange
            var practiceLog = new PracticeLog
            {
                TotalNumber = totalNumber,
                SuccessfulNumber = successfulNumber
            };

            float actual = 0;

            // Act
            if (practiceLog.TotalNumber.HasValue && practiceLog.SuccessfulNumber.HasValue && practiceLog.TotalNumber.Value > 0)
            {
                actual = (float)practiceLog.SuccessfulNumber.Value / practiceLog.TotalNumber.Value * 100;
            }
            else
            {
                actual = 0;
            }

            // Assert 
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 0)]
        public void CalculatePercentage_ZeroOrNoSuccessful_ReturnsZeroPercentage(int totalNumber, int successfulNumber)
        {
            // Arrange
            var practiceLog = new PracticeLog
            {
                TotalNumber = totalNumber,
                SuccessfulNumber = successfulNumber
            };

            float actual = 0;
            float expected = 0;

            // Act
            if (practiceLog.TotalNumber.HasValue && practiceLog.SuccessfulNumber.HasValue && practiceLog.TotalNumber.Value > 0)
            {
                actual = (float)practiceLog.SuccessfulNumber.Value / practiceLog.TotalNumber.Value * 100;
            }
            else
            {
                actual = 0;
            }

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
            var practiceLog = new PracticeLog
            {
                TotalNumber = totalNumber,
                SuccessfulNumber = successfulNumber
            };

            float expected = 0;
            float actual = 0;

            // Act
            if (practiceLog.TotalNumber.HasValue && practiceLog.SuccessfulNumber.HasValue && practiceLog.TotalNumber.Value > 0)
            {
                actual = (float)practiceLog.SuccessfulNumber.Value / practiceLog.TotalNumber.Value * 100;
            }
            else
            {
                actual = 0;
            }


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
            var practiceLog = new PracticeLog
            {
                TotalNumber = totalNumber,
                SuccessfulNumber = successfulNumber,
            };

            float actual = 0;

            // Act
            if (practiceLog.TotalNumber.HasValue && practiceLog.SuccessfulNumber.HasValue && practiceLog.TotalNumber.Value > 0)
            {
                actual = (float)practiceLog.SuccessfulNumber.Value / practiceLog.TotalNumber.Value * 100;
            }
            else
            {
                actual = 0;
            }

            // Assert
            float tolerance = 0.00001f;
            Assert.InRange(actual, expected - tolerance, expected + tolerance);
        }
    }
}