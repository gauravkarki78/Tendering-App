using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TenderingApp.Data
{
    public class Tender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TenderId { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string RefNo { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Title { get; set; }

        public DateTime NoticeDate { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string TenderDetails { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string TenderDoc { get; set; }

        public DateTime EndDate { get; set; }

        [Display(Name = "Organization")]
        public string OrganizationName { get; set; }

        [ForeignKey("OrganizationName")]
        public virtual Organization Organization { get; set; }

    }
}
