using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using CompanyWebsite.Authorization.Roles;
using CompanyWebsite.Authorization.Users;
using CompanyWebsite.MultiTenancy;
using CompanyWebsite.Careers;
using CompanyWebsite.Applicants;
using CompanyWebsite.Documents;
using CompanyWebsite.SendEmail;

namespace CompanyWebsite.EntityFrameworkCore
{
    public class CompanyWebsiteDbContext : AbpZeroDbContext<Tenant, Role, User, CompanyWebsiteDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public virtual DbSet<Career> Careers { get; set; }
        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<EmailQueues> EmailQueues { get; set; }

        public CompanyWebsiteDbContext(DbContextOptions<CompanyWebsiteDbContext> options)
            : base(options)
        {
        }
    }
}
