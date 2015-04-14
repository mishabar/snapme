using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens.Api;

namespace SNAPME.Web.Controllers.Api
{
    //[Authorize]
    [RoutePrefix("api/v1")]
    public class ProductController : ApiController
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
    }
}
