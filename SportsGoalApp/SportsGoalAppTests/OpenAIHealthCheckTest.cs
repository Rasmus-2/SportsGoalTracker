using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsGoalAppTests
{
    public class OpenAIHealthCheckTest : IClassFixture<WebApplicationFactory<OpenAIApi.Program>>
    {
        
        private readonly HttpClient _httpClient;
        public OpenAIHealthCheckTest(WebApplicationFactory<OpenAIApi.Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task HealthCheckReturnsHealthy()
        {
            // Arrange
            // Act
            var response = await _httpClient.GetAsync("/health");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("Healthy", await response.Content.ReadAsStringAsync());
        }

    }
}
