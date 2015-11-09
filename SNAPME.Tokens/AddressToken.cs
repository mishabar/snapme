using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SNAPME.Data;

namespace SNAPME.Tokens
{
    public class AddressToken
    {
        public string name { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public int zip_code { get; set; }
    }

    public static class AddressTokenExtensions
    {
        public static AddressToken AsToken(this Address address)
        {
            return new AddressToken 
            {
                line1 = address.line1,
                line2 = address.line2,
                name = address.name,
                city = address.city,
                state = address.state,
                country = address.country,
                zip_code = address.zip_code
            };
        }

        public static Address AsAddress(this AddressToken address)
        {
            return new Address
            {
                line1 = address.line1,
                line2 = address.line2,
                name = address.name,
                city = address.city,
                state = address.state,
                country = address.country,
                zip_code = address.zip_code
            };
        }
    }
}
