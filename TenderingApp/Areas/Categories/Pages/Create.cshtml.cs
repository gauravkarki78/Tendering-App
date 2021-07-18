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

namespace TenderingApp.Areas.Categories.Pages
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
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var categoryItemFromDb = await _context.Category.FindAsync(Category.CategoryName);

            //Console.WriteLine(files.Count);

            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, @"images\Category\");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, Category.CategoryName + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                categoryItemFromDb.Icon = @"\images\Category\" + Category.CategoryName + extension;
            }
            else
            {

                
                //no file was uploaded, so use default
                var uploads = Path.Combine(webRootPath, @"images\Category\" + SD.DefaultIcon);
                System.IO.File.Copy(uploads, webRootPath + @"\images\Category\" + Category.CategoryName + ".jpg");
                
                categoryItemFromDb.Icon = @"\images\Category\" + Category.CategoryName + ".jpg";
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
