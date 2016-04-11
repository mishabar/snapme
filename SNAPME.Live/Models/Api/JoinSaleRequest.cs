using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNAPME.Live.Models.Api
{
    public class JoinSaleRequest
    {
        public long product_id { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public double shipping_price { get; set; }
        public string stripe_token { get; set; }
        public string source_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip_code { get; set; }
    }
}