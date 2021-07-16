using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TenderingApp.Data;

namespace TenderingApp.Areas.Organizations.Pages
{
    public class EditModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public EditModel(TenderingApp.Data.ApplicationDbContext context)
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
           ViewData["CategoryName"] = new SelectList(_context.Category, "CategoryName", "CategoryName");
           ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "SubCategoryId", "SubCategoryId");
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

            _context.Attach(Organization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(Organization.OrganizationName))
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

        private bool OrganizationExists(string id)
        {
            return _context.Organization.Any(e => e.OrganizationName == id);
        }
    }
}
