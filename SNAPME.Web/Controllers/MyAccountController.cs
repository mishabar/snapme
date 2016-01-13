using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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

        [AllowAnonymous]
        public RedirectResult Image(string id)
        {
            var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
            return Redirect(user.ImageUrl ?? "http://graph.facebook.com/2/picture?type=square");
        }
    }
}