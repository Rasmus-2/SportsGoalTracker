using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        //public ChartController(global::SportsGoalAppTests.ISportsGoalAppContext @object)
        //{
        //    Object = @object;
        //}

        //public global::SportsGoalAppTests.ISportsGoalAppContext Object { get; }

        [HttpGet("data")]
        public IActionResult GetChartData()
        {
            //Fetch Percentage data directly from the PracticeLog table
            var data = _context.PracticeLog
                .Select(pl => pl.Percentage)
                .ToList();

            return Ok(data);
        }
    }
}
