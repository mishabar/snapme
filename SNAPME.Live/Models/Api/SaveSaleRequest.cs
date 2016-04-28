using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNAPME.Live.Models.Api
{
    public class SaveSaleRequest
    {
        public long product_id { get; set; }
        public long sale_id { get; set; }
        public double target { get; set; }
        public SaleType sale_type { get; set; }
        public int stock { get; set; }
        public int required_snaps { get; set; }
        public DateTime starts_on { get; set; }
        public DateTime ends_on { get; set; }
    }
}