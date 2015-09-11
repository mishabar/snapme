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

            public void Reset() 
            {
                price = msrp;
                drops = 0;
                progress = 0;
            }
        }

        private static Sale[] sales = new Sale[] {
                    new Sale{ id = "55b817638e659b2438f1df4e", name = "Bose SoundLink Mini", image_url = "http://worldwide.bose.com/axa/assets/images/products/soundlink_mini/soundlink_mini_si_lg_2.png", msrp= 175.00, target = 129.00, price = 175.00, drops = 0, progress = 0 },
                    new Sale{ id =  "1234567891", name =  "Intel NUC", image_url =  "http://www.pcper.com/files/imagecache/article_max_width/news/2015-01-06/nuc_01.png", msrp =  499.99, target =  129.99, price = 499.99, drops =  0, progress =  0 },
                    new Sale{ id =  "1234567892", name =  "KitchenAid Stand Mixer", image_url =  "http://www.theproductpedia.com/wp-content/uploads/2013/11/kitchenaid-stand-mixer.jpg", msrp =  125.00, target =  79.00, price =  125.00, drops =  0, progress =  0 },
                    new Sale{ id =  "1234567893", name =  "KitchenAid Ceramic 4.2 - Quart Casserole Dish with Lid", image_url =  "http://www.kitchenaid.com/digitalassets/KBLR42CRER/Standalone_1175X1290.jpg", msrp =  99.00, target =  69.00, price = 99.00, drops =  0, progress =  0 }
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
                if (rnd.NextDouble() <= 0.4F)
                {
                    int drops = rnd.Next(5);
                    sale.price -= 0.3F * (double)drops;
                    sale.drops += drops;
                    sale.progress = (int)Math.Floor((sale.msrp - sale.price) / (sale.msrp - sale.target) * 100);
                }
            }
            return Ok(sales);
        }
    }
}
