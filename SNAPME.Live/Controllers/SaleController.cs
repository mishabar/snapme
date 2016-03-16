using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.Live.Controllers
{
    public class SaleController : Controller
    {
        // GET: Join
        [Route("sale/{id}/join")]
        public PartialViewResult Join()
        {
            return PartialView("_Join");
        }
    }
}