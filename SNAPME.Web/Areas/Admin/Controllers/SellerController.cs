﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;
using SNAPME.Tokens.Admin;
using SNAPME.Web.Areas.Admin.Models;
using Microsoft.AspNet.Identity.Owin;
using SNAPME.Web.Models;
using System.Threading.Tasks;
using SNAPME.Web.Helpers;
using SNAPME.Web.Models.Emails;

namespace SNAPME.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SellerController : Controller
    {
        private ISellerService _sellerService;
        private IIDService _idService;
        private IEmailService _emailService;

        public SellerController(ISellerService sellerService, IIDService idService, IEmailService emailService)
        {
            _sellerService = sellerService;
            _idService = idService;
            _emailService = emailService;
        }

        // GET: Admin/Seller
        public ActionResult Index()
        {
            return View(_sellerService.GetAll().OrderBy(s => s.trading_name));
        }

        // GET: Admin/Seller
        public ActionResult Create()
        {
            return View(new SellerToken { id = null });
        }

        // GET: Admin/Seller/Edit/{id}
        public async Task<ActionResult> Edit(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var seller = _sellerService.GetById(id);
            ApplicationUser sellerUser = await userManager.FindByEmailAsync(seller.email);
            seller.has_account = (sellerUser != null);
            return View(seller);
        }

        // GET: Admin/Seller/Details/{id}
        public ActionResult Details(string id)
        {
            return PartialView("_Details", _sellerService.GetById(id));
        }

        // GET: Admin/Seller/Products/{id}
        public ActionResult Products(string id)
        {
            return PartialView("_Products", _sellerService.GetAllProducts(id));
        }

        // GET: Admin/Seller/Sales/{id}
        public ActionResult Sales(string id)
        {
            return PartialView("_Sales");
        }

        // GET: Admin/Seller/Product/{id}
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateAccount(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var seller = _sellerService.GetById(id);
            ApplicationUser sellerUser = await userManager.FindByEmailAsync(seller.email);

            if (sellerUser == null)
            {
                sellerUser = new ApplicationUser { UserName = seller.email, Email = seller.email };
                string password = System.Web.Security.Membership.GeneratePassword(10, 0);
                var result = await userManager.CreateAsync(sellerUser, password);
                if (result.Succeeded)
                {
                    // Add Seller role
                    result = await userManager.AddToRoleAsync(sellerUser.Id, "Seller");
                    result = await userManager.AddClaimAsync(sellerUser.Id, new System.Security.Claims.Claim("urn:iisnap:name", seller.name));
                    result = await userManager.AddClaimAsync(sellerUser.Id, new System.Security.Claims.Claim("urn:iisnap:seller_id", seller.id));
                    var renderer = new ViewRenderer();
                    var body = renderer.RenderViewToString("~/Views/Emails/_SellerWelcome.cshtml",
                        new SellerWelcomeModel { Email = seller.email, Name = seller.name, Password = password });

                    _emailService.Send(seller.email, "Welcome to iiSnap", body);
                    return Json(new { result = "User Created" });
                }
            }

            return Json(new { error = "User Already Exists" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Archive(string id)
        {
            try
            {
                _sellerService.Archive(id);
                // Check with Ami whether account should be deleted as well / or at least locked

                return Json(new { result = "Seller Archived" });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public PartialViewResult Accounts(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return PartialView("_Accounts", userManager.Users.Where(u => u.Claims.Any(c => c.Type == "urn:iisnap:seller_id" && c.Value == id)));
        }

        public PartialViewResult ResetPassword(string email)
        {
            return PartialView("_ResetPassword", new ResetPasswordViewModel { Email = email });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = await userManager.FindByEmailAsync(model.Email);
                model.Code = await userManager.GeneratePasswordResetTokenAsync(user.Id);
                var result = await userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return Json(new { result = "Password Set" });
                }
                return Json(new { error = result.Errors.First() });
            }

            return Json(new { result = "Bad Request" });
        }
    }
}