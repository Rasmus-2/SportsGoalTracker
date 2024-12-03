using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Models;

namespace SportsGoalApp.Pages
{
    public class PracticeLogModel : PageModel
    {
        private readonly Data.SportsGoalAppContext _context;
        private UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager { get; set; }

        public Areas.Identity.Data.SportsGoalAppUser MyUser { get; set; }

        public PracticeLogModel(Data.SportsGoalAppContext context, UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager)
        {
            _context = context;
            PracticeLog = new PracticeLog();
            _userManager = userManager;
        }

        [BindProperty]
        public PracticeLog PracticeLog { get; set; }
        public List<Goal> Goals { get; set; }

        public async Task OnGetAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);

            if (MyUser != null)
            {
                Goals = await _context.Goals
                    .Where(g => g.UserId == MyUser.Id)
                    .OrderByDescending(m => m.StartDate)
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Ensure the UserId is being correctly assigned
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Handle the case where the user is not found
                return Unauthorized();
            }

            // Ensure the user ID is assigned properly
            PracticeLog.UserId = user.Id;

            // Set other properties and calculate percentage
            CalculatePercentage();

            // Create the new practice log entry
            var newPracticeLog = new PracticeLog
            {
                UserId = user.Id,
                GoalId = PracticeLog.GoalId,
                Notes = PracticeLog.Notes,
                DateTime = PracticeLog.DateTime,
                Duration = PracticeLog.Duration,
                DurationUnit = PracticeLog.DurationUnit,
                Activity = PracticeLog.Activity,
                TotalNumber = PracticeLog.TotalNumber,
                SuccessfulNumber = PracticeLog.SuccessfulNumber,
                Percentage = PracticeLog.Percentage,
            };

            _context.Practices.Add(newPracticeLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public void CalculatePercentage()
        {

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
