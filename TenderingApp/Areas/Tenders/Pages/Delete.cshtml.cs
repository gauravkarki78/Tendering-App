using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TenderingApp.Data;

namespace TenderingApp.Areas.Tenders.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public DeleteModel(TenderingApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tender Tender { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tender = await _context.Tender
                .Include(t => t.Organization).FirstOrDefaultAsync(m => m.TenderId == id);

            if (Tender == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tender = await _context.Tender.FindAsync(id);

            if (Tender != null)
            {
                _context.Tender.Remove(Tender);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
