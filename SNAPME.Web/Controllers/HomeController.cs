using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Tokens;
using SNAPME.Web.Models.Home;

namespace SNAPME.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomepageModel model = new HomepageModel
            {
                Categories = CategoryToken.Generate(4),
                EndingSoon = SaleToken.Generate(1, 1000).First(),
                Featured = SaleToken.Generate(4, 1000)
            };
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult HowItWorks()
        {
            return View();
        }
    }
}