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


        public ActionResult Details()
        {
            return View(new MyAccountDetailsModel { ActiveSection = AccountMenuSection.Details });
        }

        public ActionResult Address()
        {
            return View(new MyAccountAddressModel { ActiveSection = AccountMenuSection.Address });
        }

        public ActionResult Points()
        {
            return View(new MyAccountPointsModel { ActiveSection = AccountMenuSection.Points });
        }

        public ActionResult Drops()
        {
            int drops = new Random().Next(10);
            return View(new MyAccountDropsModel
            {
                ActiveSection = AccountMenuSection.Drops,
                DropsCount = drops,
                Drops = SaleToken.Generate(drops, 1000)
            });
        }

        public ActionResult Snaps()
        {
            int snaps = new Random().Next(10);
            return View(new MyAccountSnapsModel
            {
                ActiveSection = AccountMenuSection.Snaps,
                SnapsCount = snaps,
                Snaps = SaleToken.GenerateEnded(snaps)
            });
        }

        public ActionResult Favorites()
        {
            return View(new MyAccountFavoritesModel
            {
                ActiveSection = AccountMenuSection.Favorites,
                Favorites = _productService.GetFavorites(User.Identity.GetUserId())
            });
        }
    }
}