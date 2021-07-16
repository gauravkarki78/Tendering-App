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
    public class IndexModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public IndexModel(TenderingApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Organization> Organization { get;set; }

        public async Task OnGetAsync()
        {
            Organization = await _context.Organization
                .Include(o => o.Category)
                .Include(o => o.SubCategory).ToListAsync();
        }
    }
}
