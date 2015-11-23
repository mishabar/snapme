using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;

namespace SNAPME.Tokens
{
    public class UserSnapToken
    {
        public string product_id { get; set; }
        public string name { get; set; }
        public float msrp { get; set; }
        public float price { get; set; }
        public float close_price { get; set; }
        public int sale_state { get; set; }
        public bool checkout_completed { get; set; }
        public DateTime snapped_at { get; set; }
        public int bonus_points { get; set; }
    }

    public static class UserSnapTokenExtensions
    {
        public static UserSnapToken AsToken(this UserSnap snap)
        {
            return new UserSnapToken 
            {
                product_id = snap.product_id,
                name = snap.product_name,
                sale_state = snap.sale_state,
                checkout_completed = snap.checkout_completed,
                msrp = (float)snap.msrp / 100F,
                price = (float)snap.price / 100F,
                close_price = (float)snap.close_price / 100F,
                snapped_at = snap.snapped_at,
                bonus_points = snap.bonus_points
            };
        }
    }
}
