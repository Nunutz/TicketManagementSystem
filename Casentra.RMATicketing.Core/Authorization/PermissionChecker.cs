using Abp.Authorization;
using Casentra.RMATicketing.Authorization.Roles;
using Casentra.RMATicketing.MultiTenancy;
using Casentra.RMATicketing.Users;

namespace Casentra.RMATicketing.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
