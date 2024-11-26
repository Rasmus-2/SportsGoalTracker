using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Areas.Identity.Data;

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


        public async Task OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SportsGoalAppUser user = await _userManager.GetUserAsync(User);
            NewGoal.UserId = user.Id;
            NewGoal.Category = 1;

            _context.Goal.Add(NewGoal);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
