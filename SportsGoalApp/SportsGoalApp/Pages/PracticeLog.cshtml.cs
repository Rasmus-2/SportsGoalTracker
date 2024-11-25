using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsGoalApp.Models;

namespace SportsGoalApp.Pages
{
    public class PracticeLogModel : PageModel
    {
        private readonly Data.SportsGoalAppContext _context;

        public PracticeLogModel(Data.SportsGoalAppContext context)
        {
            _context = context;
            PracticeLog = new PracticeLog();
        }

        [BindProperty]
        public PracticeLog PracticeLog { get; set; }
        public List<Goal> Goals { get; set; }

        public void OnGet()
        {
            // TODO: change to "_context.Goals.ToList();" when having the context

            Goals = new List<Goal>
            {
                new Goal { Id = 1, Title = "Slap shot training" },
                new Goal { Id = 2, Title = "Passing accuracy training" },
                new Goal { Id = 3, Title = "Cardio training" }
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            CalculatePercentage();

            _context.Practices.Add(PracticeLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public void CalculatePercentage() {
            
            if (PracticeLog.TotalNumber.HasValue && PracticeLog.SuccessfulNumber.HasValue && PracticeLog.TotalNumber.Value > 0)
            {
                PracticeLog.Percentage = (float)PracticeLog.SuccessfulNumber / PracticeLog.TotalNumber * 100;
            }
            else
            {
                PracticeLog.Percentage = 0;
            }
        }
    }
}
