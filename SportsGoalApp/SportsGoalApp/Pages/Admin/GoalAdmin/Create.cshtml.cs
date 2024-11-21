using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsGoalApp.Data;
using SportsGoalApp.Models;

namespace SportsGoalApp.Pages.Admin.GoalAdmin
{
    public class CreateModel : PageModel
    {
        private readonly SportsGoalApp.Data.SportsGoalAppContext _context;

        public CreateModel(SportsGoalApp.Data.SportsGoalAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Goal Goal { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Goals.Add(Goal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
