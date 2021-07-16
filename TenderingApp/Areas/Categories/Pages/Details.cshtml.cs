﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TenderingApp.Data;

namespace TenderingApp.Areas.Categories.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly TenderingApp.Data.ApplicationDbContext _context;

        public DetailsModel(TenderingApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(m => m.CategoryName == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
