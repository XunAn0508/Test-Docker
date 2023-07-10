using System.Threading.Tasks;
using Abp.Application.Services;
using CompanyWebsite.Authorization.Accounts.Dto;

namespace CompanyWebsite.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
