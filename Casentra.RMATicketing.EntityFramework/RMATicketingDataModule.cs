using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Casentra.RMATicketing.EntityFramework;

namespace Casentra.RMATicketing
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(RMATicketingCoreModule))]
    public class RMATicketingDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<RMATicketingDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
