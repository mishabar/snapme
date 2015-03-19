using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Web.Areas.Seller.Models;

namespace SNAPME.Web.Areas.Seller.Controllers
{
    public class HomeController : Controller
    {
        // GET: Seller/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(SellerRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Registered");
            }
            return View(model);
        }

        public ActionResult Registered()
        {
            return View();
        }
    }
}