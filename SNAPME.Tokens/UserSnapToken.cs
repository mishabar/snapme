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
        public int sale_state { get; set; }
        public bool checkout_completed { get; set; }
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
                checkout_completed = snap.checkout_completed
            };
        }
    }
}
