using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SNAPME.Tokens;

namespace SNAPME.Web.Models.Home
{
    public class HomepageModel
    {
        public IEnumerable<ProductToken> ActiveSales { get; set; }

        public IEnumerable<ProductToken> FavoriteProducts { get; set; }
    }
}