using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Text;
using System.Text;
using System.Text.Json;

namespace SportsGoalApp.Pages
{
    public class AICoachModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public string Completion { get; private set; }

        public AICoachModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task OnGetAsync()
        {
            var payload = new { RawInput = "I practice hockey shots today, but I didn't feel good. I'm not sure I can handle this" };
            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44334/AICoach/coachingAdvice", content);

            if(response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<SentencePayloadResponse>(responseString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Completion = responseJson?.CoachingAdvice ?? "No response from the API";

            }
            else
            {
                Completion = "Error fixing sentence";
            }
        }

        private class SentencePayloadResponse
        {
            public string CoachingAdvice { get; set; }
        }
        
    }
}
