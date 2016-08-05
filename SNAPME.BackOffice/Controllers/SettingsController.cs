using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.BackOffice.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            if (Request.UrlReferrer == null)
            {
                return Redirect("/Portal");
            }
            return PartialView();
        }
    }
}