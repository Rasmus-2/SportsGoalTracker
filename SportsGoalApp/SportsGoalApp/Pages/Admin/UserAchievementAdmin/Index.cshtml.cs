using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Data;
using SportsGoalApp.Models;

namespace SportsGoalApp.Pages.Admin.UserAchievementAdmin
{
    public class IndexModel : PageModel
    {
        private readonly SportsGoalApp.Data.SportsGoalAppContext _context;

        public IndexModel(SportsGoalApp.Data.SportsGoalAppContext context)
        {
            _context = context;
        }

        public IList<UserAchievement> UserAchievement { get;set; } = default!;

        public async Task OnGetAsync()
        {
            UserAchievement = await _context.UserAchievements.ToListAsync();
        }
    }
}
