using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Controllers;
using SportsGoalApp.Data;
using SportsGoalApp.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsGoalAppTests
{

    public class ChartControllerIntegrationTests
    {
        private ISportsGoalAppContext GetInMemoryContext()

        {
            var options = new DbContextOptionsBuilder<SportsGoalAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .EnableSensitiveDataLogging()
                .Options;

            var context = new SportsGoalAppContext(options);

            // Seed the database with test data
            context.Practices.AddRange(/*new List<PracticeLog>*/
            
              new PracticeLog { Percentage = 75, UserId = "user1" },
              new PracticeLog { Percentage = 85, UserId = "user2" },
              new PracticeLog { Percentage = 90, UserId = "user3" }
            );

            context.SaveChanges();

            return context;
        }

        [Fact]
        public void GetChartData_ReturnsCorrectData()
        {
            // Arrange
            var context = GetInMemoryContext();
            var controller = new ChartController(context);

            // Act
            var result = controller.GetChartData(); 
            var okResult = result as OkObjectResult;


            // Assert
            Assert.NotNull(okResult);
            var list = okResult.Value as IEnumerable;
            var data = new List<float>();
            foreach( var item in list)
            {
                data.Add(float.Parse(item.ToString()));
            }
            Assert.NotNull(data);
            Assert.Equal(3, data.Count);
            Assert.Contains(75, data);
            Assert.Contains(85, data);
            Assert.Contains(90, data);
        }
    }
}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Xunit;
//using SportsGoalApp;
//using Microsoft.EntityFrameworkCore.InMemory;
//using SportsGoalApp.Data;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.EntityFrameworkCore;
//using SportsGoalApp.Pages;
//using SportsGoalApp.Models;
//using SportsGoalApp.Services;
//using SportsGoalApp.Areas.Identity.Data;

//namespace SportsGoalAppTests
//{
//    public class ChartControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
//    {
//        private readonly HttpClient _httpClient;
//        private readonly WebApplicationFactory<Program> _applicationFactory;

//        public ChartControllerIntegrationTest(WebApplicationFactory<Program> applicationFactory)
//        {

//            _applicationFactory = applicationFactory.WithWebHostBuilder(builder =>
//            {
//                builder.ConfigureServices(services =>
//                {
//                    Remove existing DbContex registration
//                    var decriptor = services.SingleOrDefault(
//                        d => d.ServiceType == typeof(DbContextOptions<SportsGoalAppContext>));

//                    if (decriptor != null)
//                    {
//                        services.Remove(decriptor);
//                    }

//                    Add a new DbContext using an in-memory database
//                    services.AddDbContext<SportsGoalAppContext>(options =>
//                    {
//                        options.UseInMemoryDatabase("SportsGoalAppTestDatabase");
//                    });

//                    Remove existing DbContex registration
//                    decriptor = services.SingleOrDefault(
//                        d => d.ServiceType == typeof(DbContextOptions<ForTestingDbContext>));

//                    if (decriptor != null)
//                    {
//                        services.Remove(decriptor);
//                    }

//                    Register the ForTestingDbContext
//                    services.AddDbContext<ForTestingDbContext>(options =>
//                    {
//                        options.UseInMemoryDatabase("ForTestingDatabase");
//                    });

//                    Build the service provider
//                    var serviceProvider = services.BuildServiceProvider();

//                    Create a scope to obtain a reference to the database contexts
//                    using (var scope = serviceProvider.CreateScope())
//                    {
//                        var sourceContext = scope.ServiceProvider.GetRequiredService<SportsGoalAppContext>();
//                        var destinationContext = scope.ServiceProvider.GetRequiredService<ForTestingDbContext>();

//                        Transfer data from SQL to in -memory database
//                       var dataTransferService = new DataTransferService(sourceContext, destinationContext);
//                        dataTransferService.TransferData();
//                    }

//                });

//            });
//            _httpClient = _applicationFactory.CreateClient();
//        }


//        [Fact]
//        public async Task GetChartValue_ReturnOk_WithValue()
//        {
//            Arrange
//            var request = "/api/chart/data";

//            Act
//            var response = await _httpClient.GetAsync(request);

//            Assert
//            response.EnsureSuccessStatusCode();
//            var responseValue = await response.Content.ReadAsStringAsync();
//            Assert.False(string.IsNullOrEmpty(responseValue));


//        }
//    }
//}
