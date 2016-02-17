using Newtonsoft.Json;
using SNAPME.Services.Interfaces;
using SNAPME.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SNAPME.Web.Controllers.Api
{
    [AllowAnonymous]
    [RoutePrefix("api/v1")]
    public class PostalController : ApiController
    {
        private IProductService _productService;

        public PostalController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("postal/quote"), HttpPost]
        public IHttpActionResult GetQuote(PostalQuoteToken token)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("AUTH-KEY", "7e0a0c60-5d2a-4fa8-983d-8fbc876de831");
                var result = wc.DownloadString(string.Format("https://auspost.com.au/api/postage/parcel/domestic/calculate.json?from_postcode=4000&to_postcode={0}&length=20&width=20&height=20&weight=2&service_code=AUS_PARCEL_REGULAR", token.zip));
                return Ok(Newtonsoft.Json.JsonConvert.DeserializeObject<object>(result));
            }
        }
    }
}
