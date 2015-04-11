using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Tokens;
using SNAPME.Web.Core;
using SNAPME.Web.Models.Sale;

namespace SNAPME.Web.Controllers
{
    public class SaleController : Controller
    {
        // GET: Drop
        [Route("sale/drop/{id}", Name = "SaleDrop"), HttpGet, EnableCompression]
        public PartialViewResult Drop(string id)
        {
            return PartialView("_Drop", new DropPriceDialogModel { Drop = (new Random().NextDouble() + 0.3F), SaleID = id, ProductName = id });
        }

        // GET: Sale
        [Route("sale/{id}", Name = "SaleDetails"), HttpGet, EnableCompression]
        public ActionResult Details(string id)
        {
            return View(SaleToken.Generate(1, 1000).First());
        }
    }
}