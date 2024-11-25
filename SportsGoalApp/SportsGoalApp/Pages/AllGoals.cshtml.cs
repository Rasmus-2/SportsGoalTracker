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
        }

        public List<Models.Goal> MyGoals { get; set; }

        public async Task OnGetAsync()
        {
            SportsGoalAppUser user = await _userManager.GetUserAsync(User);
            MyGoals = await _context.Goals.Where(u => u.UserId == user.Id).ToListAsync();
        }
    }
}
