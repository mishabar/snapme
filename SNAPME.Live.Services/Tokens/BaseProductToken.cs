using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Tokens
{
    public class BaseProductToken
    {
        public long id { get; set; }
        public long community_id { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public string images_mode { get; set; }
        public string image { get; set; }

        public double price { get; set; }
        public double target { get; set; }

        public ObjectState state { get; set; }

        public BaseProductToken(Product product)
        {
            id = product.id;
            community_id = product.community_id;

            name = product.name;
            description = product.description;
            price = product.price;
            images_mode = product.images_mode;
            image = product.images[0];

            if (product.sale != null)
            {
                target = product.sale.target;
            }
        }

        public BaseProductToken()
        {

        }
    }
}
