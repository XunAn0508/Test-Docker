using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CompanyWebsite.Configuration;
using CompanyWebsite.EntityFrameworkCore;
using CompanyWebsite.Migrator.DependencyInjection;

namespace CompanyWebsite.Migrator
{
    [DependsOn(typeof(CompanyWebsiteEntityFrameworkModule))]
    public class CompanyWebsiteMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public CompanyWebsiteMigratorModule(CompanyWebsiteEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(CompanyWebsiteMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                CompanyWebsiteConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CompanyWebsiteMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
