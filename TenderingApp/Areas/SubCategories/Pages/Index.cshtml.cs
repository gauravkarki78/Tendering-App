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
    public class IndexModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public IndexModel(TenderingApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SubCategory> SubCategory { get;set; }

        public async Task OnGetAsync()
        {
            SubCategory = await _context.SubCategory.ToListAsync();
        }
    }
}
