using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using Casentra.RMATicketing.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace Casentra.RMATicketing.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<RMATicketing.EntityFramework.RMATicketingDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RMATicketing";
        }

        protected override void Seed(RMATicketing.EntityFramework.RMATicketingDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
