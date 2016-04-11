using SNAPME.Live.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SNAPME.Live.Controllers.Api
{
    [RoutePrefix("api/v1/shipping")]
    public class ShippingController : ApiController
    {
        [Route("rates"), HttpPost]
        public async Task<IHttpActionResult> CalculateShippingRates(ShippingCalculationRequest request)
        {
            using (var wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("AUTH-KEY", "7e0a0c60-5d2a-4fa8-983d-8fbc876de831");
                    var result = wc.DownloadString(string.Format("https://auspost.com.au/api/postage/parcel/domestic/calculate.json?from_postcode={5}&to_postcode={0}&length={1}&width={2}&height={3}&weight={4}&service_code=AUS_PARCEL_REGULAR", 
                        request.to_zip, request.length, request.width, request.height, request.weight, request.zip));
                    return Ok(Newtonsoft.Json.JsonConvert.DeserializeObject<object>(result));
                }
                catch
                {
                    return BadRequest("Please verify the postal code");
                }
            }
        }
    }
}
