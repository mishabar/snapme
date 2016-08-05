using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.BackOffice.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        [Route("Products"), HttpGet]
        public ActionResult Index()
        {
            if (Request.UrlReferrer == null) 
            {
                return Redirect("/Portal");
            }
            return PartialView();
        }

        // GET: Product
        [Route("Product/{product_id}"), HttpGet]
        public ActionResult Edit(int product_id)
        {
            if (Request.UrlReferrer == null)
            {
                return Redirect("/Portal");
            }
            return PartialView();
        }
    }
}