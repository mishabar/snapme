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
using System.Threading.Tasks;
using SNAPME.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using SNAPME.Tokens.Admin;

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
                //if (User.IsInRole("Administrator")) { return Redirect("/Admin/Products"); }
                return View("Details", _sellerService.GetById((User.Identity as ClaimsIdentity).FindFirst("urn:iisnap:seller_id").Value));
            }
            
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = await userManager.FindByEmailAsync(User.Identity.GetUserName());
                if (user.Roles.Contains("Administrator"))
                {
                    return Redirect("/Admin");
                }
                else if (user.Roles.Contains("Seller"))
                {
                    return Redirect("/Seller");
                }
                else
                {
                    return Redirect("/");
                }
            }

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var result = await signInManager.PasswordSignInAsync(model.Email.Trim(), model.Password.Trim(), model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = await userManager.FindByEmailAsync(model.Email.Trim());
                    if (user.Roles.Contains("Administrator"))
                    {
                        return Redirect("/Admin");
                    }
                    else if (user.Roles.Contains("Seller"))
                    {
                        return Redirect("/Seller");
                    }
                    else
                    {
                        return Redirect("/");
                    }
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
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
                _sellerService.Save(new SellerToken {
                    name = model.FullName,
                    email = model.Email,
                    website = model.Website,
                    phone = model.CountryCode + model.PhoneNumber,
                    comments = model.Comments
                });

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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult FAQ()
        {
            return View();
        }
    }
}