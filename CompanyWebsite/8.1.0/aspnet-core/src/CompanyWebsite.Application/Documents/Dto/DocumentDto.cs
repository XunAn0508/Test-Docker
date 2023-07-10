using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CompanyWebsite.Applicants;
using CompanyWebsite.Careers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWebsite.Documents.Dto
{
    [AutoMapFrom(typeof(Document))]
    public partial class DocumentDto : EntityDto<int>
    {
        public int Id { get; set; }
        public int RefId { get; set; }
        public string RefTable { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
    }
}
