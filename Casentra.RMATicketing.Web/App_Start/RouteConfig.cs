using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Casentra.RMATicketing.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //ASP.NET Web API Route Config
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Ticket",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Private", action = "Ticket", id = UrlParameter.Optional }
            //);
        }
    }
}
