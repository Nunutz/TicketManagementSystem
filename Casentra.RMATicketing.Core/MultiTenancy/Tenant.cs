using Abp.MultiTenancy;
using Casentra.RMATicketing.Users;

namespace Casentra.RMATicketing.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}