using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Models;
using System.Text;
using System.Text.Json;

namespace SportsGoalApp.Pages
{
    [Authorize]
    public class AICoachModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly Data.SportsGoalAppContext _context;

        public string Completion { get; private set; }
        public List<CoachingAdvices> LastThreeAdvices { get; private set; }

        public AICoachModel(HttpClient httpClient, Data.SportsGoalAppContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }


        [BindProperty]
        public PracticeLog CurrentPracticeLog { get; set; }

        [BindProperty]
        public Goal CurrentGoal { get; set; }


        public async Task OnGetAsync()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            // Query for the current goal
            CurrentGoal = await _context.Goals
                .Where(g => g.StartDate <= today && g.EndDate >= today)
                .OrderBy(g => g.EndDate)
                .FirstOrDefaultAsync();

            // Latest Practicelog    
            CurrentPracticeLog = await _context.Practices
                .OrderByDescending(l => l.DateTime)
                .FirstOrDefaultAsync();


            string rawInputNotes = "";
            string rawInputGoal = "";
            var payload = new { RawInput = "" };

            if (CurrentPracticeLog != null && CurrentGoal != null)
            {
                rawInputNotes = CurrentPracticeLog.Notes;
                rawInputGoal = CurrentGoal.Description;
                payload = new { RawInput = "Give me some uplifting coaching advice for" + rawInputGoal + " with " + rawInputNotes + " as context" };
            }

            else
            {
                payload = new { RawInput = "Give me some uplifting coaching advice" };
            }

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7138/AICoach/coachingAdvice", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<SentencePayloadResponse>(responseString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Completion = responseJson?.CoachingAdvice ?? "No response from the API";

                var newAdvice = new CoachingAdvices
                {
                    UserID = User.Identity.Name,
                    Advice = Completion,
                    CreatedAt = DateTime.UtcNow
                };
                _context.CoachingAdvices.Add(newAdvice);
                await _context.SaveChangesAsync();


            }
            else
            {
                Completion = "Error fixing sentence";
            }

            LastThreeAdvices = await _context.CoachingAdvices
                .Where(a => a.UserID == User.Identity.Name)
                .OrderByDescending(a => a.CreatedAt)
                .Take(3)
                .ToListAsync();
        }

        private class SentencePayloadResponse
        {
            public string CoachingAdvice { get; set; }
        }

    }
}
