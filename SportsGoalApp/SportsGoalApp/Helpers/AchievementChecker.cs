using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Models;

namespace SportsGoalApp.Helpers
{
    public class AchievementChecker
    {
        private UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager { get; set; }
        private readonly Data.SportsGoalAppContext _context;

        public AchievementChecker(UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager, Data.SportsGoalAppContext context)
        {
            _userManager = userManager;
            _context = context;

            completedAchievementsIds = new List<bool>();

            achievementsMatrix = new string[,]
            {
                //Skill goals
                {"Skill novice", "Complete on skill goal"},
                {"Skill trainee", "Complete two skill goals"},
                {"Skill intermediate", "Complete three skill goals"},
                {"Skill expert", "Complete five skill goals"},
                {"Skill master", "Complete ten skill goals"}

                /* Unimplemented achievements

                {"Long term skills", "Complete a skill goal stretching at least three months"},
                {"Dedicated skill trainer", "Log at least 30 practices at one skill goal"},

                //Miscellaneous
                {"Never give up", "After having failed a goal in a category, have your next goal be in the same category"},
                {"All rounder", "Complete goals in three different categories"},
                {"Multitude master", "Complete goals in five different categories"},
                {"To easy", "Reach your goal in your first three sessions"},
                {"Clear path", "Improve every practice session until you reach your goal"}
                
                */
            };
        }

        public List<bool> completedAchievementsIds { get; set; }
        public string[,] achievementsMatrix { get; set; }

        public async Task CheckAll(string userId)
        {
            completedAchievementsIds.Add(await SkillNoviceCheck(userId));
            completedAchievementsIds.Add(await SkillTraineeCheck(userId));
            completedAchievementsIds.Add(await SkillIntermediateCheck(userId));
            completedAchievementsIds.Add(await SkillExpertCheck(userId));
            completedAchievementsIds.Add(await SkillMasterCheck(userId));

            await AddAchievementsToDb(userId);
        }


        private async Task<bool> SkillNoviceCheck(string userId)
        {
            List<Goal> myGoals = await _context.Goals.Where(g => g.UserId == userId && g.Success == true).ToListAsync();
            if (myGoals.Count > 0) return true;
            return false;
        }

        private async Task<bool> SkillTraineeCheck(string userId)
        {
            List<Goal> myGoals = await _context.Goals.Where(g => g.UserId == userId && g.Success == true).ToListAsync();
            if (myGoals.Count > 1) return true;
            return false;
        }

        private async Task<bool> SkillIntermediateCheck(string userId)
        {
            List<Goal> myGoals = await _context.Goals.Where(g => g.UserId == userId && g.Success == true).ToListAsync();
            if (myGoals.Count > 2) return true;
            return false;
        }

        private async Task<bool> SkillExpertCheck(string userId)
        {
            List<Goal> myGoals = await _context.Goals.Where(g => g.UserId == userId && g.Success == true).ToListAsync();
            if (myGoals.Count > 4) return true;
            return false;
        }

        private async Task<bool> SkillMasterCheck(string userId)
        {
            List<Goal> myGoals = await _context.Goals.Where(g => g.UserId == userId && g.Success == true).ToListAsync();
            if (myGoals.Count > 9) return true;
            return false;
        }

        private async Task AddAchievementsToDb(string userId)
        {
            List<UserAchievement> alreadyCompletedAchievements = await _context.UserAchievements.Where(u => u.UserId == userId).ToListAsync();
            bool achievementAlreadyInDb = false;

            for (int i = 0; i < completedAchievementsIds.Count; i++)
            {
                if (completedAchievementsIds[i] == true)
                {
                    foreach (UserAchievement uA in alreadyCompletedAchievements)
                    {
                        if (uA.AchievementId == i + 1)
                        {
                            achievementAlreadyInDb = true;
                            break;
                        }
                    }
                    if (!achievementAlreadyInDb)
                    {
                        achievementAlreadyInDb = false;
                        _context.UserAchievements.Add(new UserAchievement
                        {
                            UserId = userId,
                            AchievementId = i + 1,
                            DateAchieved = DateTime.Now,
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}