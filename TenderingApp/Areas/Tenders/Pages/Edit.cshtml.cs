using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TenderingApp.Data;

namespace TenderingApp.Areas.Tenders.Pages
{
    public class EditModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public EditModel(TenderingApp.Data.ApplicationDbContext context)
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
           ViewData["OrganizationName"] = new SelectList(_context.Organization, "OrganizationName", "OrganizationName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Tender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenderExists(Tender.TenderId))
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

        private bool TenderExists(int id)
        {
            return _context.Tender.Any(e => e.TenderId == id);
        }
    }
}
