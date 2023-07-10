using Abp.Application.Services;
using CompanyWebsite.MultiTenancy.Dto;

namespace CompanyWebsite.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

