using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Areas.Identity.Data;

namespace SportsGoalApp.Data;

public class SportsGoalAppContext : IdentityDbContext<SportsGoalAppUser>, ISportsGoalAppContext
{
    public SportsGoalAppContext(DbContextOptions<SportsGoalAppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Models.Goal> Goals { get; set; } 
    public DbSet<Models.PracticeLog> Practices { get; set; }
}
