using Microsoft.AspNetCore.Mvc;
using SportsGoalApp.Data;

namespace SportsGoalApp.Controllers
{
    public class AdviceController : ControllerBase
    {
        private readonly SportsGoalAppContext _context;

        public AdviceController(SportsGoalAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_context.CoachingAdvices.ToList());
        }

        public IActionResult Get(int id)
        {
            return Ok(_context.CoachingAdvices.FirstOrDefault(x => x.Id == id));
        }
    }
}
