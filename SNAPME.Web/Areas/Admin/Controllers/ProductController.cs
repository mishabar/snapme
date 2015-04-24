using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNAPME.Services.Interfaces;

namespace SNAPME.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(_productService.GetAll());
        }
    }
}