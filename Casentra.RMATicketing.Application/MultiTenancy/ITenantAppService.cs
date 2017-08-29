using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Casentra.RMATicketing.MultiTenancy.Dto;

namespace Casentra.RMATicketing.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
