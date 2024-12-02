using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Areas.Identity.Data;
using SportsGoalApp.Models;

namespace SportsGoalApp.Pages
{
    [Authorize]
    public class SelectedGoalModel : PageModel
    {
        private UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager { get; set; }
        private readonly Data.SportsGoalAppContext _context;

        public SelectedGoalModel(UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager, Data.SportsGoalAppContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Goal SelectedGoal { get; set; }

        [BindProperty]
        public List<PracticeLog> SelectedGoalLogs { get; set; }

        public async Task OnGetAsync(int goalId = 3)
        {
            SportsGoalAppUser user = await _userManager.GetUserAsync(User);
            var goalToShow = await _context.Goals.Where(g => g.Id == goalId).FirstOrDefaultAsync();

            if (user.Id == goalToShow.UserId)
            {
                SelectedGoal = goalToShow;
            }

            var currentGoalPracticeLogs = await _context.Practices.Where(p => p.GoalId == goalToShow.Id).ToListAsync();

            if(currentGoalPracticeLogs != null)
            {
                SelectedGoalLogs = currentGoalPracticeLogs.OrderByDescending(g => g.DateTime).ToList();
            }
        }
    }
}
