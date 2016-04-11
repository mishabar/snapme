using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

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