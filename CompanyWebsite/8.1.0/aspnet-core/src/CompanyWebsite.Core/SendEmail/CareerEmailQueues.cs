using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CompanyWebsite.Careers;

namespace CompanyWebsite.SendEmail
{
    [Table("AppEmailQueues")]
    public partial class EmailQueues : Entity<int>
    {

        public EmailQueues()
        {
            DefaultFromDisplayName = null;
            DefaultFromAddress = null;
            SmtpHost = null;
            SmtpUserName = null;
            SmtpPassword = null;
            SmtpPort = null;
            SentAt = null;
            RefNo = null;
            AttachmentPaths = null;
        }

        public int Id { get; set; }
        public string DefaultFromDisplayName { get; set; }
        public string DefaultFromAddress { get; set; }
        public string RecipientEmailAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public int? SmtpPort { get; set; }
        public DateTime? SentAt { get; set; }
        public string RefNo { get; set; }
        public string AttachmentPaths { get; set; }
    }
}
