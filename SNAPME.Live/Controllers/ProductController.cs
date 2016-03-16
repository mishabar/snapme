using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.Live.Controllers
{
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        // GET: Product
        [Route("{product}", Name = "ProductDetails")]
        public ActionResult Details(string product)
        {
            return View(new Dictionary<string, string> { { "product", product} });
        }
    }
}