using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebsite.EntityFrameworkCore
{
    public static class CompanyWebsiteDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CompanyWebsiteDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CompanyWebsiteDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
