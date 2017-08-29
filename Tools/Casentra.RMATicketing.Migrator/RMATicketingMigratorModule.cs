using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Casentra.RMATicketing.EntityFramework;

namespace Casentra.RMATicketing.Migrator
{
    [DependsOn(typeof(RMATicketingDataModule))]
    public class RMATicketingMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<RMATicketingDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}