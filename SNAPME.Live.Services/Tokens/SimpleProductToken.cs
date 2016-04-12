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
        public SaleToken sale { get; set; }

        public OriginShippingInfo shipping_info { get; set; }

        public SimpleProductToken(Product product)
            : base(product)
        {
            if (product.sale != null)
            {
                sale = new SaleToken(product.sale);
            }

            shipping_info = product.shipping_info;
        }

        public SimpleProductToken()
            : base()
        {

        }
    }
}
