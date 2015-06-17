using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;
using SNAPME.Web.Models.Home;
using Microsoft.AspNet.Identity;

namespace SNAPME.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var products = _productService.GetAllWithPreferences(User.Identity.GetUserId()).Where(p => p.images.Any(i => !string.IsNullOrWhiteSpace(i)));
            HomepageModel model = new HomepageModel
            {
                ActiveSales = products
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