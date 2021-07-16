using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TenderingApp.Data;

namespace TenderingApp.Areas.Organizations.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public DeleteModel(TenderingApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Organization Organization { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Organization = await _context.Organization
                .Include(o => o.Category)
                .Include(o => o.SubCategory).FirstOrDefaultAsync(m => m.OrganizationName == id);

            if (Organization == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Organization = await _context.Organization.FindAsync(id);

            if (Organization != null)
            {
                _context.Organization.Remove(Organization);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
