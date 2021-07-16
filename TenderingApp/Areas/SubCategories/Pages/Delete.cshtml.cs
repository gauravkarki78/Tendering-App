using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TenderingApp.Data;

namespace TenderingApp.Areas.SubCategories.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public DeleteModel(TenderingApp.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubCategory = await _context.SubCategory.FindAsync(id);

            if (SubCategory != null)
            {
                _context.SubCategory.Remove(SubCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
