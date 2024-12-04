using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using SportsGoalApp;

namespace SportsGoalAppTests
{
    public class ChartControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public ChartControllerIntegrationTest(WebApplicationFactory<Program> factory)
        { 
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetChartValue_ReturnOk_WithValue()
        {
            //Arrange
            var request = "/api/chart/data";

            //Act
            var response = await _httpClient.GetAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseValue = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrEmpty(responseValue));


        }
    }
}
