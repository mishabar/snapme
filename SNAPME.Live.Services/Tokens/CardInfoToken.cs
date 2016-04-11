using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Tokens
{
    public class CardInfoToken
    {
        public string id { get; set; }
        public string brand { get; set; }
        public string last4 { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string expiration { get; set; }

        public CardInfoToken(Stripe.StripeCard c)
        {
            id = c.Id;
            brand = c.Brand;
            last4 = c.Last4;
            name = c.Name;
            address = string.Format("{0}\r\n{1}, {2} {3}, {4}", c.AddressLine1, c.AddressCity, c.AddressState, c.AddressZip, c.AddressCountry);
            expiration = string.Format("{0}/{1}", c.ExpirationMonth, c.ExpirationYear);
        }
    }
}
