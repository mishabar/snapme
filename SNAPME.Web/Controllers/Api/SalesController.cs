using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SNAPME.Services.Interfaces;
using SNAPME.Tokens;
using SNAPME.Web.Models.Api.Sales;
using Microsoft.AspNet.Identity;

namespace SNAPME.Web.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class SalesController : ApiController
    {
        private ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [Route("sales/active"), HttpGet]
        public IHttpActionResult ListActive(ListProductsRequest request)
        {
            return Ok(new { a = User.Identity.IsAuthenticated, sales = _saleService.GetAllActive(User.Identity.IsAuthenticated ? User.Identity.GetUserId() : string.Empty) });
        }

        [Route("sale/{productId}"), HttpGet]
        public IHttpActionResult GetSale(string productId)
        {
            return Ok(new { a = User.Identity.IsAuthenticated, sale = _saleService.GetScheduledSale(productId, User.Identity.IsAuthenticated ? User.Identity.GetUserId() : string.Empty) });
        }

        [Route("sale"), HttpPost]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult SaveSale(SaleToken token)
        {
            if (ModelState.IsValid)
            {
                _saleService.SaveSale(token);

                return Ok(token);
            }

            return BadRequest(ModelState);
        }

        [Route("sale/join"), HttpPost]
        [Authorize]
        public IHttpActionResult JoinSale(BaseSaleToken token)
        {
            if (ModelState.IsValid)
            {
                return Ok(_saleService.JoinSale(User.Identity.GetUserId(), token.id));
            }

            return BadRequest(ModelState);
        }

        [Route("sales/status"), HttpGet]
        public IHttpActionResult SalesStatus()
        {
            _saleService.AdjustSalesStatus();

            return Ok();
        }
    }
}
