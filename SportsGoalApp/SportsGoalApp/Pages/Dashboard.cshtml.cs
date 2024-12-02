using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Models;


namespace SportsGoalApp.Pages
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        public Areas.Identity.Data.SportsGoalAppUser MyUser { get; set; }

        private readonly UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager;

        private readonly Data.SportsGoalAppContext _context;

        [BindProperty]
        public Goal Goal {  get; set; }

        [BindProperty]
        public Goal CurrentGoal { get; set; }
        public List<Models.Goal> GoalList { get; set; }
        
        [BindProperty]
        public Models.PracticeLog PracticeLog { get; set; }
        public List<Models.PracticeLog> PracticeLogList { get; set; }


        public DashboardModel(UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager, Data.SportsGoalAppContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task  OnGetAsync()
        {

            MyUser = await _userManager.GetUserAsync(User);

            CurrentGoal = await _context.Goals
                .Where( g => g.StartDate <= DateTime.Now.Date && g.EndDate >= DateTime.Now.Date)
                .OrderBy( g => g.EndDate)
                .FirstOrDefaultAsync();
            GoalList = await _context.Goals.Where(g => g.StartDate >= DateTime.Now).ToListAsync();
            PracticeLogList = await _context.Practices.ToListAsync();

        }
    }
}
