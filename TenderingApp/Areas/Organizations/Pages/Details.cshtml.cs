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
    public class DetailsModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public DetailsModel(TenderingApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
