using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNAPME.Live.Areas.Admin.Controllers
{
    [Authorize(Roles = "iiAdmin"), RouteArea("Admin", AreaPrefix = "Admin")]
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        [Route("Products")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Product/{id}
        [Route("Product/{id}")]
        public ActionResult Edit(long id)
        {
            return View(new Dictionary<string, object> { { "id", id } });
        }
    }
}