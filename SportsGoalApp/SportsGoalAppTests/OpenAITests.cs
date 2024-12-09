using Microsoft.AspNetCore.Mvc.Testing;
using OpenAIApi;
using SportsGoalAppTests.Mock;
using System.Net.Http.Json;

namespace SportsGoalAppTests
{
    public class OpenAITests : IClassFixture<CustomWebApplicationFactory<OpenAIApi.Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public OpenAITests(CustomWebApplicationFactory<OpenAIApi.Program> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

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
            // Check the response content
            Assert.Equal("Keep practicing your shooting technique.", responseContent.CoachingAdvice);
        }

        [Fact]
        public async Task CoachingAdviceEndpoint_HandlesEdgeCaseInput()
        {
            // Arrange
            var requestPayload = new SentencePayloadRequest
            {
                RawInput = new string('a', 1000) // Very long input string
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/AICoach/coachingAdvice", requestPayload);
            var responseContent = await response.Content.ReadFromJsonAsync<SentencePayloadResponse>();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(responseContent);
            Assert.False(string.IsNullOrEmpty(responseContent.CoachingAdvice));
        }

        [Fact]
        public async Task CoachingAdviceEndpoint_ReturnsConsistentResponses()
        {
            // Arrange
            var requestPayload1 = new SentencePayloadRequest { RawInput = "Improve stamina" };
            var requestPayload2 = new SentencePayloadRequest { RawInput = "Focus on speed" };

            // Act
            var response1 = await _httpClient.PostAsJsonAsync("/AICoach/coachingAdvice", requestPayload1);
            var content1 = await response1.Content.ReadFromJsonAsync<SentencePayloadResponse>();

            var response2 = await _httpClient.PostAsJsonAsync("/AICoach/coachingAdvice", requestPayload2);
            var content2 = await response2.Content.ReadFromJsonAsync<SentencePayloadResponse>();

            // Assert
            response1.EnsureSuccessStatusCode();
            response2.EnsureSuccessStatusCode();

            Assert.NotNull(content1);
            Assert.NotNull(content2);

            Assert.False(string.IsNullOrEmpty(content1.CoachingAdvice));
            Assert.False(string.IsNullOrEmpty(content2.CoachingAdvice));
        }

    }
}
