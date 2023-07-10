using Abp.Authorization;
using CompanyWebsite.Authorization.Roles;
using CompanyWebsite.Authorization.Users;

namespace CompanyWebsite.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
