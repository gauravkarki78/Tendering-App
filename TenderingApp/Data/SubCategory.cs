using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TenderingApp.Data
{
    public class SubCategory
    {
        public SubCategory()
        {
            Icon = "~/img/SubCategory/DefaultCategory.jpg";
            Status = false;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCategoryId { get; set; }

        //Foreign key for Category
        //public string CategoryName { get; set;  }
        public Category Category { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        public string SubCategoryName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string AboutSubCategory { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Icon { get; set; }

        public bool Status { get; set; }
    }
}
