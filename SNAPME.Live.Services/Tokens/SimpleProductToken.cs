using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Tokens
{
    public class SimpleProductToken : BaseProductToken
    {
        public double current_price { get; set; }
        public int progress { get; set; }
        public int snaps_count { get; set; }
        public Snap[] snaps { get; set; }

        public OriginShippingInfo shipping_info { get; set; }

        public SimpleProductToken(Product product)
            : base(product)
        {
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

        public SimpleProductToken()
            : base()
        {

        }
    }
}
