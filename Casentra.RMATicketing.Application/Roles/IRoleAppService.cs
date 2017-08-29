using System.Threading.Tasks;
using Abp.Application.Services;
using Casentra.RMATicketing.Roles.Dto;

namespace Casentra.RMATicketing.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
