using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Tokens
{
    public class CommunityToken
    {
        public long id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public CommunityBanner banner { get; set; }

        public FeaturedProduct featured_product { get; set; }
        public IEnumerable<SimpleProductToken> products { get; set; }

        public CommunityToken(Community community, IEnumerable<Product> products)
        {
            id = community.id;
            name = community.name;
            icon = community.icon;
            banner = community.banner;
            this.products = products.Select(p => new SimpleProductToken(p));
        }
    }
}
