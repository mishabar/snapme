using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.BackOffice.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("Portal")]
        public ActionResult Portal()
        {
            return View();
        }

        public PartialViewResult Default()
        {
            if (Request.IsAuthenticated)
            {
                return PartialView();
            }
            else
            {
                return PartialView("MerchantLanding");
            }
        }

        public ActionResult GetStarted()
        {
            if (Request.UrlReferrer == null)
            {
                return Redirect("/");
            }
            return PartialView();
        }

        public ActionResult Settings()
        {
            if (Request.UrlReferrer == null)
            {
                return Redirect("/Portal");
            }
            return PartialView();
 
        }
    }
}
