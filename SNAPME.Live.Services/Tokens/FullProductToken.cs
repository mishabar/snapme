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
        public string full_details { get; set; }

        public FullProductToken(Product product)
            : base(product)
        {
            images = product.images;
            full_details = product.full_details;
        }

        public FullProductToken()
            : base()
        {

        }

        public Product AsProduct()
        {
            return new Product
            {
                id = this.id,
                name = this.name,
                description = this.description,
                full_details = this.full_details,
                images_mode = this.images_mode,
                images = this.images.Where(i => !string.IsNullOrWhiteSpace(i)).ToArray(),
                shipping_info = this.shipping_info
            };
        }
    }
}
