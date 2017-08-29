using System;
using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;
using Casentra.RMATicketing.Web.App_Start;
using System.Web.Mvc;
using System.Web;

namespace Casentra.RMATicketing.Web
{
    public class MvcApplication : AbpWebApplication<RMATicketingWebModule>
    {
        protected void Application_BeginRequest()
        {
            //if (!Context.Request.IsSecureConnection)
            //    Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));
        }
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig("log4net.config")
            );

            base.Application_Start(sender, e);           

        }
        protected void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            
            //if (!HttpContext.Current.Request.IsSecureConnection
            //    && (!HttpContext.Current.Request.IsAuthenticated))
            //{
            //    Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
            //}
            //if (HttpContext.Current.Request.IsSecureConnection
            //    && !HttpContext.Current.Request.IsAuthenticated
            //    )
            //{
            //    Response.Redirect("http://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
            //}
        }

    }
}
