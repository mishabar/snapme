using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;
using SNAPME.Web.Core;

namespace SNAPME.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ISaleService _saleService;

        public ProductController(IProductService productService, ISaleService saleService)
        {
            _productService = productService;
            _saleService = saleService;
        }

        // GET: Product
        [Route("products", Name = "ListProducts"), HttpGet, EnableCompression]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product
        [Route("product/{id}", Name = "ProductDetails"), HttpGet, EnableCompression]
        public ActionResult Details(string id)
        {
            var product = _productService.GetById(id);
            if (User.Identity.IsAuthenticated)
            {
                product.UserPreferences = _productService.GetPreferences(id, User.Identity.GetUserId());
            }
            product.Sale = _saleService.GetActive(id);

            return View(product);
        }
    }
}