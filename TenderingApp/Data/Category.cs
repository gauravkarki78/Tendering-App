using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TenderingApp.Data
{
    public class Category
    {
        //public Category()
        //{
        //    Icon = "/images/Category/DefaultCategory.jpg";
        //    Status = false;
        //}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Key]
        [Column(TypeName = "nvarchar(50)")]
     
        public string CategoryName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string AboutCategory { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Icon { get; set; }

        public bool Status { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
        public ICollection<Organization> Organizations { get; set; }


    }
}
