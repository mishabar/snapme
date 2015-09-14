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
        private class Sale
        {
            public string id { get; set; }
            public string name { get; set; }
            public string image_url { get; set; }
            public double msrp { get; set; }
            public double target { get; set; }
            public double price { get; set; }
            public int drops { get; set; }
            public int progress { get; set; }
            public string summary { get; set; }
            public bool likes { get; set; }
            public bool favors { get; set; }
            public bool has_activity { get; set; }

            public void Reset()
            {
                price = msrp;
                drops = 0;
                progress = 0;
            }
        }

        private static Sale[] sales = new Sale[] {
                    new Sale{ id = "55b817638e659b2438f1df4e", name = "Bose SoundLink Mini", image_url = "https://40.media.tumblr.com/0930af123f313f757bb6a6c1f94ec1d1/tumblr_nrbvuyzpKU1svko7io1_500.jpg", msrp= 175.00, target = 129.00, price = 175.00, drops = 0, progress = 0, summary = "Take the sound wherever you go!", likes = true, favors = false, has_activity = true },
                    new Sale{ id =  "1234567891", name =  "Intel NUC", image_url =  "http://www.techzone.lk/wp-content/uploads/2014/11/up18.png", msrp =  499.99, target =  129.99, price = 499.99, drops =  0, progress =  0, summary = "Stunning visuals and edge-of-your-seat performance in an ultra-small package.", likes = true, favors = true, has_activity = false },
                    new Sale{ id =  "1234567892", name =  "KitchenAid Stand Mixer", image_url =  "http://www.theproductpedia.com/wp-content/uploads/2013/11/kitchenaid-stand-mixer.jpg", msrp =  125.00, target =  79.00, price =  125.00, drops =  0, progress =  0, summary = "Versatility to help you with any recipe!", likes = false, favors = false, has_activity = false },
                    new Sale{ id =  "1234567893", name =  "KitchenAid Ceramic 4.2 - Quart Casserole Dish with Lid", image_url =  "http://www.kitchenaid.com/digitalassets/KBLR42CRER/Standalone_1175X1290.jpg", msrp =  99.00, target =  69.00, price = 99.00, drops =  0, progress =  0, summary = "Trap in moisture to keep your dishes piping hot and tender, all the way to the table.", likes = false, favors = false }
            };

        private static int passes = 0;

        [Route("sales/active"), HttpGet]
        public IHttpActionResult ListActiveProducts(ListProductsRequest request)
        {
            passes++;
            bool doReset = passes == 100;
            if (doReset) { passes = 0; }
            Random rnd = new Random(DateTime.Now.Millisecond);
            foreach (var sale in sales)
            {
                if (doReset) { sale.Reset(); }
                if (rnd.NextDouble() <= 0.5F)
                {
                    int drops = rnd.Next(5);
                    sale.price -= 0.25F * (double)drops;
                    sale.drops += drops;
                    sale.progress = (int)Math.Floor((sale.msrp - sale.price) / (sale.msrp - sale.target) * 100);
                }
            }
            return Ok(new { a = User.Identity.IsAuthenticated, sales = sales });
        }
    }
}
