using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TenderingApp.Data
{
    public class Organization
    {
        public Organization()
        {

        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrganizationId { get; set; }

        [Key]
        [Column(TypeName = "nvarchar(500)")]
        public string OrganizationName { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string ContactNo { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string EmailAddress { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string ContactPerson { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string WebUrl { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Country { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string State { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string District { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Vdc { get; set; }

        public int WardNo { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string ToleName { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Landmark { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Logo { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string MapUrl { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string AboutOrg { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [ForeignKey("CategoryName")]
        public virtual Category Category { get; set; }

        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }
    }
}
