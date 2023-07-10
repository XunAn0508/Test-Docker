using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CompanyWebsite.Documents
{
    [Table("Document")]
    public partial class Document : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public int RefId { get; set; }
        public string RefTable { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
