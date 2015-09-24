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
using SNAPME.Web.Helpers;
using SNAPME.Web.Models.Api;

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

        [Route("check/product"), HttpPost, Authorize(Roles = "Administrator, Seller")]
        public IHttpActionResult Check(ProductBaseToken product)
        {
            _sellerService.CheckProduct(product.id);

            return Ok();
        }

        [Route("product/image"), HttpPost, Authorize(Roles = "Administrator, Seller")]
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

        [Route("product/feed"), HttpPost, AllowAnonymous]
        public IHttpActionResult FullSocialFeed(ProductBaseToken product)
        {
            if (ModelState.IsValid)
            {
                return Ok(_productService.GetFullSocialFeed(product.id.ToProductId()));
            }

            return BadRequest(ModelState);
        }

        [Route("product/feed/friends"), HttpPost, AllowAnonymous]
        public IHttpActionResult SocialFeed(ProductBaseToken product)
        {
            if (ModelState.IsValid)
            {
                return Ok(_productService.GetSocialFeed(product.id.ToProductId(), User.Identity.GetUserId()));
            }

            return BadRequest(ModelState);
        }

        [Route("product/{id}/{saleData?}"), HttpGet, AllowAnonymous]
        public IHttpActionResult GetProduct(string id, bool saleData)
        {
            var product = _productService.GetById(id.ToProductId());
            if (saleData)
            {
                product.sale = Sales.sales.FirstOrDefault(s => s.id == id);
            }
            return Ok(product);
        }

        [Route("products"), HttpPost, Authorize(Roles = "Administrator")]
        public IHttpActionResult GetProducts(ListQueryToken token)
        {
            bool hasData = true;
            var products = _productService.GetFiltered(token.query, token.page, out hasData);

            return Ok(new { hasData = hasData, data = products });
        }
    }
}
