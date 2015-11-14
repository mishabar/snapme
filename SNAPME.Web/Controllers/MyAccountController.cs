using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;
using SNAPME.Web.Models.Account;

namespace SNAPME.Web.Controllers
{
    [Authorize]
    public class MyAccountController : Controller
    {
        private IProductService _productService;

        public MyAccountController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            return RedirectToActionPermanent("Details");
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Favorites()
        {
            return View();
        }

        public ActionResult RewardsCenter()
        {
            return View();
        }

        public ActionResult Snaps()
        {
            return View();
        }

        public ActionResult Friends()
        {
            return View();
        }
    }
}