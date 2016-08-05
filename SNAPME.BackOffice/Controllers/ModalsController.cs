using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.BackOffice.Controllers
{
    public class ModalsController : Controller
    {
        // GET: Modals
        public PartialViewResult ScheduleSale()
        {
            return PartialView();
        }
    }
}