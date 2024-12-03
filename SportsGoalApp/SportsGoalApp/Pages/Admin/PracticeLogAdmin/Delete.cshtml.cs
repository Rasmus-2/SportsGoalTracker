﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly SportsGoalApp.Data.SportsGoalAppContext _context;

        public DeleteModel(SportsGoalApp.Data.SportsGoalAppContext context)
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

            var practicelog = await _context.Practices.FirstOrDefaultAsync(m => m.Id == id);

            if (practicelog == null)
            {
                return NotFound();
            }
            else
            {
                PracticeLog = practicelog;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practicelog = await _context.Practices.FindAsync(id);
            if (practicelog != null)
            {
                PracticeLog = practicelog;
                _context.Practices.Remove(PracticeLog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
