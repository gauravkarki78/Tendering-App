using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TenderingApp.Data;

namespace TenderingApp.Areas.SubCategories.Pages
{
    public class EditModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public EditModel(TenderingApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SubCategory SubCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubCategory = await _context.SubCategory.FirstOrDefaultAsync(m => m.SubCategoryId == id);

            if (SubCategory == null)
            {
                return NotFound();
            }
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

            _context.Attach(SubCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(SubCategory.SubCategoryId))
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

        private bool SubCategoryExists(int id)
        {
            return _context.SubCategory.Any(e => e.SubCategoryId == id);
        }
    }
}
