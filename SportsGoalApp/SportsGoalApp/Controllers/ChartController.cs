using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Data;
using SportsGoalApp.Models;


namespace SportsGoalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly ISportsGoalAppContext _context;


        public ChartController(ISportsGoalAppContext context)
        {
            _context = context;
        }

        //public async Task GetCurrentUser()
        //{
        //    MyUser = await _userManager.GetUserAsync(User);
        //}

        //public async Task<int> GetCurrentGoalId()
        //{
        //    GetCurrentUser();
        //    List<Goal> goalList = await _context.Goals.Where(g => g.UserId == MyUser.Id).ToListAsync();
        //    return await  GetCurrentCoalIdFromList(goalList);
        //}

        //public async Task<int> GetCurrentCoalIdFromList(List<Goal> goalList)
        //{
        //    return 4;
        //}


        [HttpGet("data")]
        public IActionResult GetChartData()
        {
            //int goalId = GetCurrentGoalId();
            //Fetch Percentage data directly from the PracticeLog table
            var data = _context.Practices
                .Select(pl => pl.Percentage)
                .ToList();

            return Ok(data);
        }
    }
}
