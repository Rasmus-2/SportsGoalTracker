using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Areas.Identity.Data;

namespace SportsGoalApp.Pages
{
    [Authorize]
    public class AllGoalsModel : PageModel
    {
        private UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager { get; set; }
        private readonly Data.SportsGoalAppContext _context;

        public AllGoalsModel(UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager, Data.SportsGoalAppContext context)
        {
            _userManager = userManager;
            _context = context;
            OldGoals = new List<Models.Goal>();
            CurrentGoals = new List<Models.Goal>();
            FutureGoals = new List<Models.Goal>();
        }

        public List<Models.Goal> MyGoals { get; set; }
        public List<Models.Goal> OldGoals { get; set; }
        public List<Models.Goal> CurrentGoals {  get; set; }
        public List<Models.Goal> FutureGoals {  get; set; }

        public async Task<IActionResult> OnGetAsync(int showGoalId, int deleteGoalId)
        {
            SportsGoalAppUser user = await _userManager.GetUserAsync(User);
            MyGoals = await _context.Goals.Where(g => g.UserId == user.Id).ToListAsync();

            if(showGoalId != 0)
            {
                return RedirectToPage("./SelectedGoal", "OnGetAsync", new { goalId = showGoalId });
            }

            if(deleteGoalId != 0)
            {
                var goalToDelete = MyGoals.Where(g => g.Id == deleteGoalId).FirstOrDefault();
                _context.Goals.Remove(goalToDelete);
                _context.SaveChanges();
                MyGoals.Remove(goalToDelete);
            }

            foreach (var goal in MyGoals)
            {
                if (goal.EndDate < DateOnly.FromDateTime(DateTime.Now))
                {
                    OldGoals.Add(goal);
                }
                else if (goal.StartDate > DateOnly.FromDateTime(DateTime.Now))
                {
                    FutureGoals.Add(goal);
                }
                else
                {
                    CurrentGoals.Add(goal);
                }
            }

            return Page();
        }
    }
}
