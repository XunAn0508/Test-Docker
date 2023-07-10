using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CompanyWebsite.EntityFrameworkCore;
using CompanyWebsite.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace CompanyWebsite.Web.Tests
{
    [DependsOn(
        typeof(CompanyWebsiteWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class CompanyWebsiteWebTestModule : AbpModule
    {
        public CompanyWebsiteWebTestModule(CompanyWebsiteEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CompanyWebsiteWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(CompanyWebsiteWebMvcModule).Assembly);
        }
    }
}