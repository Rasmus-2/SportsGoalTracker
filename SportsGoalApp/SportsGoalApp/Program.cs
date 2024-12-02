using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Data;
using SportsGoalApp.Utilities;
namespace SportsGoalApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("SportsGoalAppContextConnection") ?? throw new InvalidOperationException("Connection string 'SportsGoalAppContextConnection' not found.");

            builder.Services.AddDbContext<SportsGoalAppContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<Areas.Identity.Data.SportsGoalAppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<SportsGoalAppContext>();

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<ICalculatePercentage, PercentageCalculator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
