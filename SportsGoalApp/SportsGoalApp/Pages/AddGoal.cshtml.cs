using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Areas.Identity.Data;
using SportsGoalApp.Enums;
using SportsGoalApp.Models;

namespace SportsGoalApp.Pages
{
    [Authorize]
    public class AddGoalModel : PageModel
    {
        private UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager { get; set; }
        private readonly Data.SportsGoalAppContext _context;

        public AddGoalModel(UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager, Data.SportsGoalAppContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Models.Goal NewGoal { get; set; }

        public string ErrorMessage { get; set; }

        [BindProperty]
        public List<string> MyCategories { get; set; }

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

            _context.Goals.Add(NewGoal);
            await _context.SaveChangesAsync();

            if (!forbiddenDate)
            {
                _context.Goals.Add(NewGoal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./AddGoal", "OnGetAsync", new { dateError = forbiddenDate });
        }
    }
}
