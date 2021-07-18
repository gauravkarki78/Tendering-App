using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TenderingApp.Data;

using TenderingApp.Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TenderingApp.Areas.Organizations.Pages
{
    public class EditModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EditModel(TenderingApp.Data.ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            OrganizationVM = new OrganizationViewModel()
            {
                Category = _context.Category,
                Organization = new Data.Organization()
            };
        }

        [BindProperty]
        public OrganizationViewModel OrganizationVM { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Organization = await _context.Organization
            //     .Include(o => o.Category)
            //     .Include(o => o.SubCategory).FirstOrDefaultAsync(m => m.OrganizationName == id);

            // if (Organization == null)
            // {
            //     return NotFound();
            // }
            //ViewData["CategoryName"] = new SelectList(_context.Category, "CategoryName", "CategoryName");
            //ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "SubCategoryId", "SubCategoryId");

            OrganizationVM.Organization = await _context.Organization.Include(o => o.Category).Include(s => s.SubCategory).SingleOrDefaultAsync(m => m.OrganizationName == id);
            OrganizationVM.SubCategory = await _context.SubCategory.Where(s => s.CategoryName == OrganizationVM.Organization.CategoryName).ToListAsync();
            
            if (OrganizationVM.Organization == null)
            {
                return NotFound();
            }
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //_context.Attach(Organization).State = EntityState.Modified;

            OrganizationVM.Organization.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

            if (!ModelState.IsValid)
            {
                OrganizationVM.SubCategory = await _context.SubCategory.Where(s => s.CategoryName == OrganizationVM.Organization.CategoryName).ToListAsync();
                return Page();
            }

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var organizationItemFromDb = await _context.Organization.FindAsync(OrganizationVM.Organization.OrganizationName);

            if (files.Count > 0)
            {
                //New Image has been uploaded
                var uploads = Path.Combine(webRootPath, "images/Organization/");
                var extension_new = Path.GetExtension(files[0].FileName);

                //Delete the original file
                var imagePath = Path.Combine(webRootPath, organizationItemFromDb.Logo.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                //will upload the new file
                using (var filesStream = new FileStream(Path.Combine(uploads, OrganizationVM.Organization.OrganizationName + extension_new), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                organizationItemFromDb.Logo = @"\images\Organization\" + OrganizationVM.Organization.OrganizationName + extension_new;
            }

            organizationItemFromDb.ContactNo = OrganizationVM.Organization.ContactNo;
            organizationItemFromDb.EmailAddress = OrganizationVM.Organization.EmailAddress;
            organizationItemFromDb.ContactPerson = OrganizationVM.Organization.ContactPerson;
            organizationItemFromDb.WebUrl = OrganizationVM.Organization.WebUrl;
            organizationItemFromDb.Country = OrganizationVM.Organization.Country;
            organizationItemFromDb.State = OrganizationVM.Organization.State;
            organizationItemFromDb.District = OrganizationVM.Organization.District;
            organizationItemFromDb.Vdc = OrganizationVM.Organization.Vdc;
            organizationItemFromDb.WardNo = OrganizationVM.Organization.WardNo;
            organizationItemFromDb.ToleName = OrganizationVM.Organization.ToleName;
            organizationItemFromDb.Landmark = OrganizationVM.Organization.Landmark;
            organizationItemFromDb.MapUrl = OrganizationVM.Organization.MapUrl;
            organizationItemFromDb.AboutOrg = OrganizationVM.Organization.AboutOrg;
            organizationItemFromDb.CategoryName = OrganizationVM.Organization.CategoryName;
            organizationItemFromDb.SubCategoryId = OrganizationVM.Organization.SubCategoryId;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(OrganizationVM.Organization.OrganizationName))
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
