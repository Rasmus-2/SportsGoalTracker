using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsGoalApp.Data;
using SportsGoalApp.Models;

namespace SportsGoalApp.Pages.Admin.PracticeLogAdmin
{
    public class EditModel : PageModel
    {
        private readonly SportsGoalApp.Data.SportsGoalAppContext _context;

        public EditModel(SportsGoalApp.Data.SportsGoalAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PracticeLog PracticeLog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practicelog =  await _context.Practices.FirstOrDefaultAsync(m => m.Id == id);
            if (practicelog == null)
            {
                return NotFound();
            }
            PracticeLog = practicelog;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PracticeLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PracticeLogExists(PracticeLog.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PracticeLogExists(int id)
        {
            return _context.Practices.Any(e => e.Id == id);
        }
    }
}
