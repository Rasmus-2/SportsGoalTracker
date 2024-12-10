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
    public class DetailsModel : PageModel
    {
        private readonly SportsGoalApp.Data.SportsGoalAppContext _context;

        public DetailsModel(SportsGoalApp.Data.SportsGoalAppContext context)
        {
            _context = context;
        }

        public UserAchievement UserAchievement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userachievement = await _context.UserAchievements.FirstOrDefaultAsync(m => m.Id == id);
            if (userachievement == null)
            {
                return NotFound();
            }
            else
            {
                UserAchievement = userachievement;
            }
            return Page();
        }
    }
}
