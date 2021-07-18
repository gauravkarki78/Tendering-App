using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TenderingApp.Data;
 
using TenderingApp.Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using TenderingApp.Utility;

namespace TenderingApp.Areas.Organizations.Pages
{
    public class CreateModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateModel(TenderingApp.Data.ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            OrganizationVM = new OrganizationViewModel()
            {
                Category = _context.Category,
                Organization = new Data.Organization()
            };
        }

        public IActionResult OnGet()
        {
            //ViewData["CategoryName"] = new SelectList(_context.Category, "CategoryName", "CategoryName");
            //ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "SubCategoryId", "SubCategoryId");
            //OrganizationVM = new OrganizationViewModel()
            //{
            //    Category = await _context.Category.ToListAsync(),
            //    Organization = new Data.Organization()
            //};
            return Page();
        }

        [BindProperty]
        public OrganizationViewModel OrganizationVM { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            OrganizationVM.Organization.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Organization.Add(Organization);
            _context.Organization.Add(OrganizationVM.Organization);
            await _context.SaveChangesAsync();

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var organizationItemFromDb = await _context.Organization.FindAsync(OrganizationVM.Organization.OrganizationName);

            Console.WriteLine(files.Count);
            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, @"images\Organization\");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, OrganizationVM.Organization.OrganizationName + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                organizationItemFromDb.Logo = @"\images\Organization\" + OrganizationVM.Organization.OrganizationName + extension;
            }
            else
            {
                //no file was uploaded, so use default
                var uploads = Path.Combine(webRootPath, @"images\Organization\" + SD.DefaultIcon);
                System.IO.File.Copy(uploads, webRootPath + @"\images\Organization\" + OrganizationVM.Organization.OrganizationName + ".jpg");
                organizationItemFromDb.Logo = @"\images\Organization\" + OrganizationVM.Organization.OrganizationName + ".jpg";
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
