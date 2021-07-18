using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TenderingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace TenderingApp.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly TenderingApp.Data.ApplicationDbContext _db;
        public SubCategoryService(TenderingApp.Data.ApplicationDbContext db)
        {
            _db = db;
        }

        public List<SubCategory> GetSubCategory(string id)
        {
            List<SubCategory> subCategories = new List<SubCategory>();

            //subCategories = from subCategory in _db.SubCategory
            //                      where subCategory.CategoryName == id
            //                      select subCategory);
            subCategories = _db.SubCategory.Where(s=>s.CategoryName == id)
                .ToList<SubCategory>();

            return subCategories;
        }
    }
}
