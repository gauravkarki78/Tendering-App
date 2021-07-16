using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TenderingApp.Data;

namespace TenderingApp.Areas.Organizations.Pages
{
    public class CreateModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public CreateModel(TenderingApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryName"] = new SelectList(_context.Category, "CategoryName", "CategoryName");
        ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "SubCategoryId", "SubCategoryId");
            return Page();
        }

        [BindProperty]
        public Organization Organization { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Organization.Add(Organization);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
