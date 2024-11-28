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
            var payload = new { RawSentence = "What are the benefits of exercise?" };
            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44334/grammarfixer/fixGrammar", content);

            if(response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<SentencePayloadResponse>(responseString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Completion = responseJson?.FixedSentence ?? "No response from the API";

            }
            else
            {
                Completion = "Error fixing sentence";
            }
        }

        private class SentencePayloadResponse
        {
            public string FixedSentence { get; set; }
        }
        
    }
}
