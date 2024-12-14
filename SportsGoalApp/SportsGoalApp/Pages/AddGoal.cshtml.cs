using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Areas.Identity.Data;
using SportsGoalApp.Enums;
using SportsGoalApp.Models;
using SportsGoalApp.Utilities;

namespace SportsGoalApp.Pages
{
    [Authorize]
    public class AddGoalModel : PageModel
    {
        private UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager { get; set; }
        private readonly Data.SportsGoalAppContext _context;
        private readonly IAvailableDateChecker _availableDateChecker;

        public AddGoalModel(UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager, Data.SportsGoalAppContext context, IAvailableDateChecker availableDateChecker)
        {
            _userManager = userManager;
            _context = context;
            _availableDateChecker = availableDateChecker;
        }

        [BindProperty]
        public Models.Goal NewGoal { get; set; }

        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(bool dateError)
        {
            if (dateError)
            {
                ErrorMessage = "You already have a goal during some of those dates, you can only have one goal at a time";
            }
            else
            {
                ErrorMessage = "";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool forbiddenDate = false;
            SportsGoalAppUser user = await _userManager.GetUserAsync(User);
            NewGoal.UserId = user.Id;

            var myGoals = await _context.Goals.Where(g => g.UserId == user.Id).ToListAsync();
            if (myGoals != null)
            {
                foreach (var goal in myGoals)
                {
                    forbiddenDate = _availableDateChecker.CheckForbiddenDate(goal.EndDate, goal.StartDate, NewGoal.StartDate, NewGoal.EndDate);
                    if (forbiddenDate) break;
                }
            }

            if (!forbiddenDate)
            {
                _context.Goals.Add(NewGoal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./AddGoal", "OnGetAsync", new { dateError = forbiddenDate });
        }
    }
}
