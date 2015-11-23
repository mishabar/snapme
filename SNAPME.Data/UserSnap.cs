using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class UserSnap
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public int msrp { get; set; }
        public string sale_id { get; set; }
        public int price { get; set; }
        public int close_price { get; set; }
        public int bonus_points { get; set; }
        public int sale_state { get; set; }
        public bool checkout_completed { get; set; }
        public DateTime snapped_at { get; set; }
    }
}
