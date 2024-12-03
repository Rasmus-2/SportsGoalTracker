using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Data;
using SportsGoalApp.Models;

namespace SportsGoalApp.Pages.Admin.PracticeLogAdmin
{
    public class IndexModel : PageModel
    {
        private readonly SportsGoalApp.Data.SportsGoalAppContext _context;

        public IndexModel(SportsGoalApp.Data.SportsGoalAppContext context)
        {
            _context = context;
        }

        public IList<PracticeLog> PracticeLog { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PracticeLog = await _context.Practices.ToListAsync();
        }
    }
}
