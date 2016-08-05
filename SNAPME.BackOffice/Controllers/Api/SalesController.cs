using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SNAPME.BackOffice.Controllers.Api
{
    [RoutePrefix("api/v1")]
    //[Authorize]
    public class SalesController : ApiController
    {
        [Route("sales/live/{category_id}")]
        public IHttpActionResult GetLiveSales(int category_id)
        {
            return Ok(new object[] { 
                new { 
                    id = 1, 
                    name = "Audi A3", 
                    description = "Third generation (Typ 8V; 2012–)", 
                    price = 38000F, 
                    images = new string[] { "http://images.cdn.autocar.co.uk/sites/autocar.co.uk/files/styles/gallery_slide/public/audi-a3-uk-1_0.jpg?itok=F-3bwGkZ" },
                    sale = new { id = 1, visits = 777, ends_in = "3 days 14 hours"}}
            });
        }

        [Route("sales/ended/{category_id}")]
        public IHttpActionResult GetEndedSales(int category_id)
        {
            return Ok(new object[] { 
                new { 
                    id = 1, 
                    name = "Audi A3", 
                    description = "Third generation (Typ 8V; 2012–)", 
                    price = 38000F, 
                    images = new string[] { "http://images.cdn.autocar.co.uk/sites/autocar.co.uk/files/styles/gallery_slide/public/audi-a3-uk-1_0.jpg?itok=F-3bwGkZ" },
                    sale = new { id = 1 }}
            });
        }
    }
}
