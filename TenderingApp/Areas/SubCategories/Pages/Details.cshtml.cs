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
    public class DetailsModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public DetailsModel(TenderingApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
