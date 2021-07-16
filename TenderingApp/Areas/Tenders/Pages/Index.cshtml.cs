using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TenderingApp.Data;

namespace TenderingApp.Areas.Tenders.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public IndexModel(TenderingApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Tender> Tender { get;set; }

        public async Task OnGetAsync()
        {
            Tender = await _context.Tender
                .Include(t => t.Organization).ToListAsync();
        }
    }
}
