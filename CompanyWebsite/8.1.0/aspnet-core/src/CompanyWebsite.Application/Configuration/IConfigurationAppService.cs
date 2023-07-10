using System.Threading.Tasks;
using CompanyWebsite.Configuration.Dto;

namespace CompanyWebsite.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
