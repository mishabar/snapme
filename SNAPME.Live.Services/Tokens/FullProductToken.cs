using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Tokens
{
    public class FullProductToken : SimpleProductToken
    {
        public string[] images { get; set; }

        public FullProductToken(Product product)
            : base(product)
        {
            images = product.images;
            if (product.sale != null)
            {
                snaps = product.sale.snaps;
            }
        }
    }
}
