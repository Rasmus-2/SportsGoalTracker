using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsGoalApp.Areas.Identity.Data;
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
            builder.Services.AddScoped<ISportsGoalAppContext, SportsGoalAppContext>();

            builder.Services.AddDefaultIdentity<Areas.Identity.Data.SportsGoalAppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<SportsGoalAppContext>();

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<ICalculatePercentage, PercentageCalculator>();
            //Cookies
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Register HttpClient
            builder.Services.AddHttpClient();

            var app = builder.Build();

            app.UseCookiePolicy();

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

            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}
