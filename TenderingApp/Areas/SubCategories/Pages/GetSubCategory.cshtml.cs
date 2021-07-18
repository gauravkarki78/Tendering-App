using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TenderingApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using TenderingApp.Services;

namespace TenderingApp.Areas.SubCategories.Pages
{
    public class GetSubCategoryModel : PageModel
    {
        //private readonly TenderingApp.Data.ApplicationDbContext _db;
        private readonly ISubCategoryService _SubCategoryService;

        //public GetSubCategoryModel(TenderingApp.Data.ApplicationDbContext db)
        //{
        //    _db = db;

        //}


        public GetSubCategoryModel(ISubCategoryService SubCategoryService)
        {
            _SubCategoryService = SubCategoryService;
        }
        //public async Task<JsonResult> OnGetAsync(string id)
        //{
        //    List<SubCategory> subCategories = new List<SubCategory>();

        //    subCategories = await (from subCategory in _db.SubCategory
        //                           where subCategory.CategoryName == id
        //                           select subCategory).ToListAsync();
        //    //foreach (var a in subCategories)
        //    //{
        //    //    Console.WriteLine(a.CategoryName);
        //    //}

        //    //return new JsonResult(new SelectList(subCategories, "Id", "Name"));
        //    return new JsonResult(new SelectList(subCategories, "Id", "Name"));
        //}
        public JsonResult OnGet(string id)
        {
            return new JsonResult(new SelectList(_SubCategoryService.GetSubCategory(id), "SubCategoryId", "SubCategoryName"));
            //return new JsonResult(_SubCategoryService.GetSubCategory(id));
            //return new JsonResult(_SubCategoryService.GetSubCategory(id));
        }
    }
}
