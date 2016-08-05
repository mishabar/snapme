using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.BackOffice.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Live()
        {
            if (Request.UrlReferrer == null)
            {
                return Redirect("/Portal");
            }
            return PartialView();
        }

        public ActionResult Schedule()
        {
            if (Request.UrlReferrer == null)
            {
                return Redirect("/Portal");
            }
            return PartialView();
        }

        public ActionResult Ended()
        {
            if (Request.UrlReferrer == null)
            {
                return Redirect("/Portal");
            }
            return PartialView();
        }
    }
}