using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;
using SNAPME.Tokens.Api;
using SNAPME.Web.Areas.Admin.Models;

namespace SNAPME.Web.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/v1")]
    public class ProductController : ApiController
    {
        private IProductService _productService;
        private ISellerService _sellerService;

        public ProductController(IProductService productService, ISellerService sellerService)
        {
            _productService = productService;
            _sellerService = sellerService;
        }

        [Route("like/product"), HttpPost]
        public IHttpActionResult LikeProduct(BaseProductToken token)
        {
            var liked = true;
            try
            {
                liked = _productService.ToggleLike(User.Identity.GetUserId(), token.id);
            }
            catch
            {
                return BadRequest();
            }

            return Json(new { liked = liked, id = token.id });
        }

        [Route("favorite/product"), HttpPost]
        public IHttpActionResult FavorProduct(BaseProductToken token)
        {
            var favored = true;
            try
            {
                favored = _productService.ToggleFavorite(User.Identity.GetUserId(), token.id);
            }
            catch
            {
                return BadRequest();
            }

            return Json(new { favored = favored, id = token.id });
        }

        [Route("save/product"), HttpPost, Authorize(Roles = "Administrator, Seller")]
        public IHttpActionResult Save(ProductToken product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sellerService.SaveProduct(product);
                    return Ok(product);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return BadRequest(ModelState);
                }
            }

            return BadRequest(ModelState);
        }

        [Route("product/image"), HttpPost, Authorize(Roles="Administrator, Seller")]
        public IHttpActionResult AddImage(ProductImageModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sellerService.SaveProductImage(model.id, model.image, model.idx);
                    return Ok();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return BadRequest(ModelState);
                }
            }

            return BadRequest(ModelState);
        }
    }
}
