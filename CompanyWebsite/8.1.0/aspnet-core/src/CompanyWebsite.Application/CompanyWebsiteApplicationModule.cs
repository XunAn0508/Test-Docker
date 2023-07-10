using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CompanyWebsite.Authorization;

namespace CompanyWebsite
{
    [DependsOn(
        typeof(CompanyWebsiteCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CompanyWebsiteApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CompanyWebsiteAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CompanyWebsiteApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
