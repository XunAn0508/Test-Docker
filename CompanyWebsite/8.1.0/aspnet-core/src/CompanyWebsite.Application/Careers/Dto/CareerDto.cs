using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CompanyWebsite.Applicants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWebsite.Careers.Dto
{
    [AutoMapFrom(typeof(Career))]
    public partial class CareerDto : EntityDto<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public string Qualifications { get; set; }
        public string Responsibilities { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
