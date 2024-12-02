using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Models;

namespace SportsGoalApp.Data
{
    public interface ISportsGoalAppContext
    {
        DbSet<PracticeLog> Practices { get; }
    }
}