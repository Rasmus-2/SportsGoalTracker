using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsGoalApp.Models;
using Xunit;
using Moq;
using SportsGoalApp.Data;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace SportsGoalAppTests
{
    public class ChartAPITest
    {
        [Fact]
        public void GetChartData_ShouldReturnPercentageData()
        {
            //Arrange
            var mockContext = new Mock<ISportsGoalAppContext>();
            var mockPracticeLog = new List<PracticeLog>
            {
                new PracticeLog{Percentage=75},
                new PracticeLog{Percentage=85},
                new PracticeLog{Percentage=90}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<PracticeLog>>();
            mockDbSet.As<IQueryable<PracticeLog>>().Setup(m => m.Provider).Returns(mockPracticeLog.Provider);
            mockDbSet.As<IQueryable<PracticeLog>>().Setup(m => m.Expression).Returns(mockPracticeLog.Expression);
            mockDbSet.As<IQueryable<PracticeLog>>().Setup(m => m.ElementType).Returns(mockPracticeLog.ElementType);
            mockDbSet.As<IQueryable<PracticeLog>>().Setup(m => m.GetEnumerator()).Returns(mockPracticeLog.GetEnumerator());

            mockContext.Setup(c => c.PracticeLog).Returns(mockDbSet.Object);

            var controller = new ChartController(mockContext.Object);

            //Act
            var result = controller.GetChartData() as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<float?>>(result.Value);
            var data= result.Value as List<float?>;
            Assert.Equal(3, data.Count);
            Assert.Contains(75, data);
            Assert.Contains(85, data);
            Assert.Contains(90, data);
        }
    }
}
