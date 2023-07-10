using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Configuration;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Net.Mail;

namespace CompanyWebsite.EntityFrameworkCore.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly CompanyWebsiteDbContext _context;

        public DefaultSettingsCreator(CompanyWebsiteDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            int? tenantId = null;

            if (CompanyWebsiteConsts.MultiTenancyEnabled == false)
            {
                tenantId = MultiTenancyConsts.DefaultTenantId;
            }

            //Email
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "8DGE", tenantId);
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "testing2023@8dge.com.my", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.Host, "mail.8dge.com.my", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.UserName, "testing2023@8dge.com.my", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.Password, "8dge@8828!", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.Port, "587", tenantId);
            AddSettingIfNotExists("RecipientEmailAddresses", "alan.cheng@8dge.com.my", tenantId);

            // Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en", tenantId);
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}