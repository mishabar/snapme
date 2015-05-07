using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.Web.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        [Route("404", Name = "FileNotFound")]
        public ActionResult FileNotFound()
        {
            return View();
        }
    }
}