using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Casentra.RMATicketing.Web.Controllers
{
      // [AbpMvcAuthorize]
    public class HomeController : RMATicketingControllerBase
    {
        public ActionResult Index()
        {
               return Redirect("/Private/Ticket");
        }
        public ActionResult TicketHome()
        {
            return View();
        }
        public ActionResult Service()
        {
            return View(); //Layout of the angular application.
        }
        public ActionResult Aboutus()
        {
            return View(); //Layout of the angular application.
        }
        public ActionResult Contactus()
        {
            return View(); //Layout of the angular application.
        }
    }
}