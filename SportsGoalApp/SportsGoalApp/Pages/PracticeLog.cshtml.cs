using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SportsGoalApp.Pages
{
    public class PracticeLogModel : PageModel
    {
        private readonly Data.SportsGoalAppContext _context;

        public PracticeLogModel(Data.SportsGoalAppContext context)
        {
            _context = context;
            PracticeLog = new Models.PracticeLog();
        }

        [BindProperty]
        public Models.PracticeLog PracticeLog { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if(PracticeLog.TotalNumber.HasValue && PracticeLog.SuccessfulNumber.HasValue && PracticeLog.TotalNumber.Value > 0)
            {
                PracticeLog.Percentage = (float)PracticeLog.SuccessfulNumber / PracticeLog.TotalNumber * 100;
            }

            _context.Practices.Add(PracticeLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
