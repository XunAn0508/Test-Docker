using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CompanyWebsite.Careers;

namespace CompanyWebsite.Applicants
{
    [Table("Applicant")]
    public partial class Applicant : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public int CareerId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Message { get; set; }
        //public string Attachment { get; set; }
        //public string Resume { get; set; }
        //public string Transcript { get; set; }
        public string Status { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("CareerId")]
        public virtual Career Career { get; set; }
    }
}
