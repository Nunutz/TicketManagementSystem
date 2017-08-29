using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Abp.Zero.Configuration;
using Casentra.RMATicketing.Api;
using Hangfire;
using Abp.Localization;
using System.Threading;
using System.Globalization;
using Casentra.RMATicketing.Web.App_Start;

namespace Casentra.RMATicketing.Web
{
    [DependsOn(
        typeof(RMATicketingDataModule),
        typeof(RMATicketingApplicationModule),
        typeof(RMATicketingWebApiModule),
        typeof(AbpWebSignalRModule),
        //typeof(AbpHangfireModule), - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
        typeof(AbpWebMvcModule))]
    public class RMATicketingWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<RMATicketingNavigationProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});

            //Configuration.Localization.Languages.Add(new LanguageInfo("fr", "Franch", "famfamfam-flag-fr", true));
            //Configuration.Localization.Languages.Add(new LanguageInfo("tr", "Türkçe", "famfamfam-flag-tr",true));
            // Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england"));

          
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           


        }
    }
}
