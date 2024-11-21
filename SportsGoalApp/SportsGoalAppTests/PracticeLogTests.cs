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
        public void CalculatePercantage_ValidInputs_ReturnExpectedPercentage(int totalNumber, int successfulNumber, float expectedPercentage)
        {
            // Arrange
            var practiceLog = new PracticeLog
            {
                TotalNumber = totalNumber,
                SuccessfulNumber = successfulNumber
            };

            // Act
            if (practiceLog.TotalNumber.HasValue && practiceLog.SuccessfulNumber.HasValue && practiceLog.TotalNumber.Value > 0)
            {
                practiceLog.Percentage = (float)practiceLog.SuccessfulNumber.Value / practiceLog.TotalNumber.Value * 100;
            }
            else
            {
                practiceLog.Percentage = 0;
            }

            // Assert 
            Assert.Equal(expectedPercentage, practiceLog.Percentage);
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

            // Act
            if (practiceLog.TotalNumber.HasValue && practiceLog.SuccessfulNumber.HasValue && practiceLog.TotalNumber.Value > 0)
            {
                practiceLog.Percentage = (float)practiceLog.SuccessfulNumber.Value / practiceLog.TotalNumber.Value * 100;
            }
            else
            {
                practiceLog.Percentage = 0;
            }

            // Assert
            Assert.Equal(0, practiceLog.Percentage);
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

            // Act
            if (practiceLog.TotalNumber.HasValue && practiceLog.SuccessfulNumber.HasValue && practiceLog.TotalNumber.Value > 0)
            {
                practiceLog.Percentage = (float)practiceLog.SuccessfulNumber.Value / practiceLog.TotalNumber.Value * 100;
            }
            else
            {
                practiceLog.Percentage = 0;
            }


            // Assert
            Assert.Equal(0, practiceLog.Percentage);
        }

        [Theory]
        [InlineData(75, 50, 66.66667)]
        [InlineData(150, 100, 66.66667)]
        [InlineData(90, 60, 66.66667)]
        [InlineData(200, 133, 66.5)]
        [InlineData(75, 25, 33.33333)]
        public void CalculatePercentage_DifferentValidInputs_ReturnsExpectedPercentageWithDecimals(int totalNumber, int successfulNumber, float expectedPercentage)
        {
            // Arrange
            var practiceLog = new PracticeLog
            {
                TotalNumber = totalNumber,
                SuccessfulNumber = successfulNumber,
            };

            // Act
            if (practiceLog.TotalNumber.HasValue && practiceLog.SuccessfulNumber.HasValue && practiceLog.TotalNumber.Value > 0)
            {
                practiceLog.Percentage = (float)practiceLog.SuccessfulNumber.Value / practiceLog.TotalNumber.Value * 100;
            }
            else
            {
                practiceLog.Percentage = 0;
            }

            // Assert
            Assert.Equal((double)expectedPercentage, (double)practiceLog.Percentage, precision: 5);
        }
    }
}