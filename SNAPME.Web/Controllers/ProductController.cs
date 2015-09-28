using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;
using SNAPME.Web.Core;
using SNAPME.Web.Helpers;

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
        [Route("demo", Name = "Demo")]
        public ActionResult Details(string id)
        {
            try
            {
#if DEBUG
                if (string.IsNullOrEmpty(id)) { id = "55b817638e659b2438f1df4e"; }
#else
                if (string.IsNullOrEmpty(id)) { id = "552532f77ef776125096266c"; } 
#endif
                id = id.ToProductId();
                var product = _productService.GetById(id);                
                return View(product);
            }
            catch
            {
                Response.StatusCode = 404;
                return View("~/Views/Errors/FileNotFound.cshtml");
            }
        }

        // GET: Image
        [Route("image/{id}/{idx}", Name = "ProductIamge"), HttpGet, EnableCompression]
        public ActionResult Image(string id, int idx)
        {
            try
            {

                var imageb64 = _productService.GetImageById(id, idx);
                MemoryStream stream = new MemoryStream();
                string[] imageParts = imageb64.Split(';');

                string contentType = imageParts[0].Substring(5);
                byte[] image = Convert.FromBase64String(imageParts[1].Substring(7));

                stream.Write(image, 0, image.Length);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                return new FileStreamResult(stream, contentType);
            }
            catch
            {
                return Redirect("/Content/Images/logo.png");
            }
        }
    }
}