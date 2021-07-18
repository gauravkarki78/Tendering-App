using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TenderingApp.Data;

namespace TenderingApp.Services
{
    public interface ISubCategoryService
    {
        List<SubCategory> GetSubCategory(string id);
    }
}
