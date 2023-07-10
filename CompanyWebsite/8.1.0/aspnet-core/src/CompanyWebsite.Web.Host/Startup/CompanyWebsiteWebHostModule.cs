using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CompanyWebsite.Configuration;

namespace CompanyWebsite.Web.Host.Startup
{
    [DependsOn(
       typeof(CompanyWebsiteWebCoreModule))]
    public class CompanyWebsiteWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CompanyWebsiteWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CompanyWebsiteWebHostModule).GetAssembly());
        }
    }
}
