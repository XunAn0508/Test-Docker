using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace CompanyWebsite.Controllers
{
    public abstract class CompanyWebsiteControllerBase: AbpController
    {
        protected CompanyWebsiteControllerBase()
        {
            LocalizationSourceName = CompanyWebsiteConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
