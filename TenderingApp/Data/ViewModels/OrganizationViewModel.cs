using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenderingApp.Data.ViewModels
{
    public class OrganizationViewModel
    {
        public Organization Organization { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<SubCategory> SubCategory { get; set; }
    }
}
