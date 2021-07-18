using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TenderingApp.Data;

using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TenderingApp.Areas.SubCategories.Pages
{
    public class EditModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EditModel(TenderingApp.Data.ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public SubCategory SubCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubCategory = await _context.SubCategory
                .Include(s => s.Category).FirstOrDefaultAsync(m => m.SubCategoryId == id);

            if (SubCategory == null)
            {
                return NotFound();
            }
           ViewData["CategoryName"] = new SelectList(_context.Category, "CategoryName", "CategoryName");
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

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var subcategoryItemFromDb = await _context.SubCategory.FindAsync(SubCategory.SubCategoryId);

            if (files.Count > 0)
            {
                //New Image has been uploaded
                var uploads = Path.Combine(webRootPath, @"images\SubCategory");
                var extension_new = Path.GetExtension(files[0].FileName);

                //Delete the original file
                var imagePath = Path.Combine(webRootPath, subcategoryItemFromDb.Icon.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                //we will upload the new file
                using (var filesStream = new FileStream(Path.Combine(uploads, SubCategory.SubCategoryId + extension_new), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                subcategoryItemFromDb.Icon = @"\images\SubCategory\" + SubCategory.SubCategoryId + extension_new;
            }

            subcategoryItemFromDb.CategoryName = SubCategory.CategoryName;
            subcategoryItemFromDb.SubCategoryName = SubCategory.SubCategoryName;
            subcategoryItemFromDb.AboutSubCategory = SubCategory.AboutSubCategory;
            subcategoryItemFromDb.Status = SubCategory.Status;

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
