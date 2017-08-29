using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using Casentra.RMATicketing.EntityFramework;

namespace Casentra.RMATicketing.Migrations
{
    public class AbpZeroDbMigrator : AbpZeroDbMigrator<RMATicketingDbContext, Configuration>
    {
        public AbpZeroDbMigrator(
            IUnitOfWorkManager unitOfWorkManager,
            IDbPerTenantConnectionStringResolver connectionStringResolver,
            IIocResolver iocResolver)
            : base(
                unitOfWorkManager,
                connectionStringResolver,
                iocResolver)
        {
        }
    }
}
