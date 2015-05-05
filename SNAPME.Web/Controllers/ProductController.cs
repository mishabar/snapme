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
            try
            {
                var product = _productService.GetById(id);
                product.SocialFeed = new FeedEntryToken[] { new FeedEntryToken { user_id = "54e8c6d88e65a93d08655aaf", username = "Michael Bar", when = DateTime.UtcNow } };
                if (User.Identity.IsAuthenticated)
                {
                    product.UserPreferences = _productService.GetPreferences(id, User.Identity.GetUserId());
                }
                product.Sale = _saleService.GetActive(id);

                return View(product);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // GET: Image
        [Route("image/{id}/{idx}", Name = "ProductIamge"), HttpGet, EnableCompression]
        public ActionResult Image(string id, int idx)
        {
            try
            {

                var product = _productService.GetById(id);
                MemoryStream stream = new MemoryStream();
                string[] imageParts = product.images[idx].Split(';');

                string contentType = imageParts[0].Substring(5);
                byte[] image = Convert.FromBase64String(imageParts[1].Substring(7));

                stream.Write(image, 0, image.Length);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                return new FileStreamResult(stream, contentType);
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}