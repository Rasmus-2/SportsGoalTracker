using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

        public List<Models.Goal> AllGoals { get; set; }

        public async Task OnGetAsync()
        {
            AllGoals = await _context.Goals.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            NewGoal.Category = 1;
            _context.Goals.Add(NewGoal);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
