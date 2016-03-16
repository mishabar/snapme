using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.Live.Controllers
{
    [RoutePrefix("modals")]
    public class ModalsController : Controller
    {
        // GET: HowTo
        [Route("howto")]
        public ActionResult HowTo()
        {
            return PartialView();
        }
    }
}