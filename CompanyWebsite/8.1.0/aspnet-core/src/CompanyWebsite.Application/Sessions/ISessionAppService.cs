using System.Threading.Tasks;
using Abp.Application.Services;
using CompanyWebsite.Sessions.Dto;

namespace CompanyWebsite.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
