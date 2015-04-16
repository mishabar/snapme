using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Services.Interfaces;
using SNAPME.Web.Areas.Seller.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using SNAPME.Tokens;

namespace SNAPME.Web.Areas.Seller.Controllers
{
    [Authorize(Roles = "Seller")]
    public class HomeController : Controller
    {
        private ISellerService _sellerService;
        private IIDService _idService;

        public HomeController(ISellerService sellerService, IIDService idService)
        {
            _sellerService = sellerService;
            _idService = idService;
        }

        [AllowAnonymous]
        // GET: Seller/Home
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("Details", _sellerService.GetById((User.Identity as ClaimsIdentity).FindFirst("urn:iisnap:seller_id").Value));
            }
            
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(SellerRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Registered");
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Registered()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            return PartialView("_Details", _sellerService.GetById(id));
        }

        [HttpGet]
        public ActionResult Product(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PartialView("_Product", new ProductToken { is_draft = true, id = _idService.GenerateId() });
            }
            else
            {
                return PartialView("_Product", _sellerService.GetProduct(id));
            }
        }
        [HttpGet]
        public ActionResult Products(string id)
        {
            return PartialView("_Products", _sellerService.GetAllProducts(id));
        }
    }
}