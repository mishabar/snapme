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
        private ISaleService _saleService;
        private ISellerService _sellerService;

        public ProductController(IProductService productService, ISellerService sellerService, ISaleService saleService)
        {
            _productService = productService;
            _sellerService = sellerService;
            _saleService = saleService;
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

        [Route("favor/product"), HttpPost]
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
                product.sale = _saleService.GetScheduledSale(id, User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null);
            }
            return Ok(product);
        }

        [Route("product/comment"), HttpPost, Authorize]
        public IHttpActionResult SaveComment(ProductCommentToken token)
        {
            return Ok(_productService.AddComment(User.Identity.GetUserId(), User.Identity.GetUserName(), token.id, token.comment));
        }

        [Route("products"), HttpPost, Authorize(Roles = "Administrator")]
        public IHttpActionResult GetProducts(ListQueryToken token)
        {
            bool hasData = true;
            var products = _productService.GetFiltered(token.query, token.page, out hasData);

            return Ok(new { hasData = hasData, data = products });
        }

        [Route("product"), HttpPost, Authorize(Roles = "Administrator")]
        public IHttpActionResult GetProducts(ProductToken token)
        {
            _productService.SaveProduct(token);

            return Ok(token);
        }

        [Route("product/image"), HttpPost, Authorize(Roles = "Administrator")]
        public IHttpActionResult GetProducts(UploadImageToken token)
        {
            _productService.SaveImage(token.id, token.idx, token.stream);

            return Ok();
        }
    }
}
