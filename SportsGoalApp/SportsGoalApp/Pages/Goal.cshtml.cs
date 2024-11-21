using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Areas.Identity.Data;

namespace SportsGoalApp.Pages
{
    public class GoalModel : PageModel
    {
        private UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager { get; set; }
        private readonly Data.SportsGoalAppContext _context;

        public GoalModel(UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager, Data.SportsGoalAppContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Models.Goal NewGoal { get; set; }

        public List<Models.Goal> MyGoals { get; set; }

        public async Task OnGetAsync()
        {
            SportsGoalAppUser user = await _userManager.GetUserAsync(User);
            MyGoals = await _context.Goals.Where(u => u.Id == user.Id).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SportsGoalAppUser user = await _userManager.GetUserAsync(User);
            NewGoal.UserId = user.Id;
            NewGoal.Category = 1;

            _context.Goals.Add(NewGoal);
            await _context.SaveChangesAsync();
            
            return RedirectToPage();
        }
    }
}
