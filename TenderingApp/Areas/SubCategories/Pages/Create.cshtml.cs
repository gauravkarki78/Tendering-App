using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TenderingApp.Data;

using Microsoft.AspNetCore.Hosting;
using System.IO;
using TenderingApp.Utility;

namespace TenderingApp.Areas.SubCategories.Pages
{
    public class CreateModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CreateModel(TenderingApp.Data.ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryName"] = new SelectList(_context.Category, "CategoryName", "CategoryName");
            return Page();
        }

        [BindProperty]
        public SubCategory SubCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SubCategory.Add(SubCategory);
            await _context.SaveChangesAsync();

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var subcategoryItemFromDb = await _context.SubCategory.FindAsync(SubCategory.SubCategoryId);

            //Console.WriteLine(files.Count);

            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, @"images\SubCategory");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, SubCategory.SubCategoryId + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                subcategoryItemFromDb.Icon = @"\images\SubCategory\" + SubCategory.SubCategoryId + extension;
            }
            else
            {


                //no file was uploaded, so use default
                var uploads = Path.Combine(webRootPath, @"images\SubCategory\" + SD.DefaultIcon);
                System.IO.File.Copy(uploads, webRootPath + @"\images\SubCategory\" + SubCategory.SubCategoryId + ".jpg");

                subcategoryItemFromDb.Icon = @"\images\SubCategory\" + SubCategory.SubCategoryId + ".jpg";
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
