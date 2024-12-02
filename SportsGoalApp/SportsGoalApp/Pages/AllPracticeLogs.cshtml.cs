using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Models;
using System.Security.Claims;

namespace SportsGoalApp.Pages
{
    public class AllPracticeLogsModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager;
        private readonly Data.SportsGoalAppContext _context;

        public AllPracticeLogsModel(UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager, Data.SportsGoalAppContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public List<PracticeLog> MyPracticeLogs { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                MyPracticeLogs = new List<PracticeLog>();
                return;
            }

            MyPracticeLogs = await _context.Practices.Where(u => u.UserId == userId).ToListAsync();
        }
    }
}
