using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.Live.Controllers
{
    public class SnapBoxAppController : Controller
    {
        // GET: SnapBoxApp
        [Route("snapboxapp")]
        public ActionResult Index()
        {
            return View();
        }
    }
}