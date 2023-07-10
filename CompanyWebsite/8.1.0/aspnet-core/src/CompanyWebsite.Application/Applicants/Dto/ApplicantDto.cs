using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompanyWebsite.Applicants.Dto
{
    [AutoMapFrom(typeof(Applicant))]
    public partial class ApplicantDto : EntityDto<int>
    {
        public int Id { get; set; }
        public int CareerId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Message { get; set; }
        //public string Resume { get; set; }
        //public string Transcript { get; set; }
        //public string Status { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
    }
}
