using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Data;
using SportsGoalApp.Models;

namespace SportsGoalApp.Pages.Admin.GoalAdmin
{
    public class DeleteModel : PageModel
    {
        private readonly SportsGoalApp.Data.SportsGoalAppContext _context;

        public DeleteModel(SportsGoalApp.Data.SportsGoalAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Goal Goal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals.FirstOrDefaultAsync(m => m.Id == id);

            if (goal == null)
            {
                return NotFound();
            }
            else
            {
                Goal = goal;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals.FindAsync(id);
            if (goal != null)
            {
                Goal = goal;
                _context.Goals.Remove(Goal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
