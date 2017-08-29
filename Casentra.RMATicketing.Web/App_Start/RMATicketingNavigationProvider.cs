using Abp.Application.Navigation;
using Abp.Localization;
using Casentra.RMATicketing.Authorization;

namespace Casentra.RMATicketing.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class RMATicketingNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        new LocalizableString("HomePage", RMATicketingConsts.LocalizationSourceName),
                        url: "#/",
                        icon: "fa fa-home"
                        )
                )
             //.AddItem(
             //    new MenuItemDefinition(
             //        "Users",
             //        L("Users"),
             //        url: "#users",
             //        icon: "fa fa-users",
             //        requiredPermissionName: PermissionNames.Pages_Users
             //        )
             // )

               .AddItem(
                    new MenuItemDefinition(
                        "Private",
                        new LocalizableString("Private", RMATicketingConsts.LocalizationSourceName),
                        url: "#/dashboard",
                        icon: "fa fa-first-order"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Professional",
                        new LocalizableString("Professional", RMATicketingConsts.LocalizationSourceName),
                        url: "#/professional",
                        icon: "fa fa-first-order"
                        )
                )
               .AddItem(
                    new MenuItemDefinition(
                        "Trading",
                        new LocalizableString("Trading", RMATicketingConsts.LocalizationSourceName),
                        url: "#/trading",
                        icon: "fa fa-first-order"
                        )
                )         
               .AddItem(
                    new MenuItemDefinition(
                        "Imei Number",
                        new LocalizableString("ImeiNumbers", RMATicketingConsts.LocalizationSourceName),
                        url: "#/imei",
                        icon: "fa fa-info"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Data Entry",
                        new LocalizableString("DataEntry", RMATicketingConsts.LocalizationSourceName),
                        url: "#/dataentry",
                        icon: "fa fa-wpforms"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Report",
                        new LocalizableString("Report", RMATicketingConsts.LocalizationSourceName),
                        url: "#/report",
                        icon: "fa fa-gitlab"
                        )
                )
                ;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, RMATicketingConsts.LocalizationSourceName);
        }
    }
}
