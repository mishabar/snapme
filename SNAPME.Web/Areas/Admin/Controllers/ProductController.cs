using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;

namespace SNAPME.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Admin/Products
        [Route("Products")]
        public ActionResult Index()
        {
            return View(_productService.GetAll());
        }

        // GET: Admin/Product/{id}
        [Route("Product/{id?}")]
        public ActionResult Create(string id)
        {
            return View(new ProductToken { id = id });
        }
    }
}