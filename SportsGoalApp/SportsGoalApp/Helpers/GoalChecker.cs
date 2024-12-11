using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Models;

namespace SportsGoalApp.Helpers
{
    public class GoalChecker
    {
        private UserManager<Areas.Identity.Data.SportsGoalAppUser> _userManager { get; set; }
        private readonly Data.SportsGoalAppContext _context;

        public GoalChecker(UserManager<Areas.Identity.Data.SportsGoalAppUser> userManager, Data.SportsGoalAppContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> CheckIfAchieved(int? goalId)
        {
            List<PracticeLog> practiceLogs = await _context.Practices.Where(p => p.GoalId == goalId).ToListAsync();
            if (practiceLogs.Count > 2)
            {
                practiceLogs = practiceLogs.OrderByDescending(p => p.DateTime).ToList();
                if (practiceLogs.Count > 3)
                {
                    practiceLogs.RemoveRange(3, practiceLogs.Count - 3);
                }

                Goal goal = await _context.Goals.Where(g => g.Id == goalId).FirstOrDefaultAsync();
                if (goal != null)
                {
                    float? goalNumber = goal.GoalNumber;
                    foreach (var log in practiceLogs)
                    {
                        if (log.Percentage < goalNumber)
                        {
                            return false;
                        }
                    }
                goal.Success = true;
                _context.Goals.Update(goal);
                await _context.SaveChangesAsync();
                return true;
                }
            }
            return false;
        }
    }
}
