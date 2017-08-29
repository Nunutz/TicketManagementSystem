using System.Linq;
using Casentra.RMATicketing.EntityFramework;
using Casentra.RMATicketing.MultiTenancy;

namespace Casentra.RMATicketing.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly RMATicketingDbContext _context;

        public DefaultTenantCreator(RMATicketingDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
