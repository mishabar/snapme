using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Tokens
{
    public class SimpleProductToken
    {
        public long id { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string images_mode { get; set; }
        public string image { get; set; }

        public double target { get; set; }
        public double current_price { get; set; }
        public int progress { get; set; }
        public int snaps_count { get; set; }
        public Snap[] snaps { get; set; }

        public ShippingInfo shipping_info { get; set; }

        public SimpleProductToken(Product product)
        {
            id = product.id;
            name = product.name;
            description = product.description;
            price = product.price;
            images_mode = product.images_mode;
            image = product.images[0];

            if (product.sale != null)
            {
                target = product.sale.target;
                current_price = product.sale.current_price;
                progress = product.sale.progress;
                snaps_count = product.sale.snaps_count;
                snaps = product.sale.snaps.Take(25).ToArray();
            }

            shipping_info = product.shipping_info;
        }
    }
}
