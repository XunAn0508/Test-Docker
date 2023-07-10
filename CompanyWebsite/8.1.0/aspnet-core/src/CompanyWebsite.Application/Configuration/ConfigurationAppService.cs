using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using CompanyWebsite.Configuration.Dto;

namespace CompanyWebsite.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : CompanyWebsiteAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
