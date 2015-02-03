using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SNAPME.Tokens;

namespace SNAPME.Web.Models.Home
{
    public class HomepageModel
    {
        public IEnumerable<CategoryToken> Categories { get; set; }

        public SaleToken EndingSoon { get; set; }

        public IEnumerable<SaleToken> Featured { get; set; }
    }
}