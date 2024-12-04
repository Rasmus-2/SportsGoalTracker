using Microsoft.AspNetCore.Mvc.Testing;
using OpenAIApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SportsGoalAppTests
{
    public class OpenAITests : IClassFixture<WebApplicationFactory<OpenAIApi.Program>>
    {
        private readonly HttpClient _httpClient;

        public OpenAITests(WebApplicationFactory<OpenAIApi.Program> factory)
        {
            _httpClient = factory.CreateClient();

        }

        [Fact]
        public async Task CanGetVersion()
        {
            // Arrange
            var expectedVersion = "1.0";

            // Act
            var response = await _httpClient.GetAsync("/AICoach/version");
            var version = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedVersion, version);
        }

        [Fact]
        public async Task CanGetCoachingAdviceFromPrompt()
        {
            // Arrange
            var requestPayload = new SentencePayloadRequest
            {
                RawInput = "I want to improve my basketball shooting"
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/AICoach/coachingAdvice", requestPayload);
            var responseContent = await response.Content.ReadFromJsonAsync<SentencePayloadResponse>();

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(responseContent);
            Assert.False(string.IsNullOrEmpty(responseContent.CoachingAdvice));
        }
    }
}
