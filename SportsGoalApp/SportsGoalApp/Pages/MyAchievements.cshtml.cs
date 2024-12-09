using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsGoalApp.Areas.Identity.Data;

namespace SportsGoalApp.Pages
{
    [Authorize]
    public class MyAchievementsModel : PageModel
    {
        private UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager { get; set; }
        private readonly Data.SportsGoalAppContext _context;

        public MyAchievementsModel(UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager, Data.SportsGoalAppContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public List<bool> CompletedAchievementsIds { get; set; }
        public string[,] AchievementsMatrix { get; set; }

        public async Task OnGetAsync()
        {
            SportsGoalAppUser user = await _userManager.GetUserAsync(User);

            Helpers.AchievementChecker achievementChecker = new Helpers.AchievementChecker(_userManager, _context);
            await achievementChecker.CheckAll(user.Id);

            AchievementsMatrix = achievementChecker.achievementsMatrix;
            CompletedAchievementsIds = achievementChecker.completedAchievementsIds;
        }
    }
}
