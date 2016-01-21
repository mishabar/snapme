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
using System.Threading.Tasks;

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

        [AllowAnonymous]
        [Route("Start")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<ProductToken> products = await Task.Run( () => _productService.GetAllWithPreferences(User.Identity.GetUserId()).Where(p => p.images.Any(i => !string.IsNullOrWhiteSpace(i))).Take(10));
            IEnumerable<ProductToken> favoriteProducts = null;
            if (Request.IsAuthenticated)
                favoriteProducts = await Task.Run( () => _productService.GetFavorites(User.Identity.GetUserId()));

            HomepageModel model = new HomepageModel
            {
                ActiveSales = products,
                FavoriteProducts = favoriteProducts,
                Categories = _productService.GetCategories()
            };
            return View(model);
        }

        [Route("Invite")]
        public ActionResult Invite()
        {
            return View();
        }

        [Route("About")]
        public ActionResult About()
        {
            return View();
        }

        [Route("ContactUs")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("FAQ")]
        public ActionResult FAQ()
        {
            return View();
        }

        [Route("HowItWorks")]
        public ActionResult HowItWorks()
        {
            return View();
        }

        [Route("Terms")]
        public ActionResult Terms()
        {
            return View();
        }

        [Route("Privacy")]
        public ActionResult Privacy()
        {
            return View();
        }

        [Route("Seller")]
        public ActionResult Seller()
        {
            return View();
        }
    }
}