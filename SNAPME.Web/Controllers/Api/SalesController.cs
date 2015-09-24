using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SNAPME.Web.Models.Api.Sales;

namespace SNAPME.Web.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class SalesController : ApiController
    {
        private static int passes = 0;

        [Route("sales/active"), HttpGet]
        public IHttpActionResult ListActive(ListProductsRequest request)
        {
            passes++;
            bool doReset = passes == 100;
            if (doReset) { passes = 0; }
            Random rnd = new Random(DateTime.Now.Millisecond);
            foreach (var sale in Sales.sales)
            {
                if (doReset) { sale.Reset(); }
                if (rnd.NextDouble() <= 0.5F)
                {
                    int drops = rnd.Next(5);
                    sale.price -= (sale.msrp - sale.target) / 200F * (double)drops;
                    sale.drops += drops;
                    sale.progress = (int)Math.Floor((sale.msrp - sale.price) / (sale.msrp - sale.target) * 100);
                }
            }
            return Ok(new { a = User.Identity.IsAuthenticated, sales = Sales.sales });
        }

        [Route("sale/{productId}"), HttpGet]
        public IHttpActionResult GetSale(string productId)
        {
            return Ok(new { a = User.Identity.IsAuthenticated, sale = Sales.sales.FirstOrDefault(s => s.id == productId) });
        }
    }
}
