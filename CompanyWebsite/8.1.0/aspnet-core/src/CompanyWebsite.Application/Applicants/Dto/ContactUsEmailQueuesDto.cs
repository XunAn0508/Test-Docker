using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CompanyWebsite.SendEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompanyWebsite.Applicants.Dto
{
    [AutoMapFrom(typeof(ContactUsEmailQueues))]

    public partial class ContactUsEmailQueuesDto : EntityDto<int>
    {
       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

    }
}
