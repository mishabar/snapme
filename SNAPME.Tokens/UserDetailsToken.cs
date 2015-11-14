using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data;

namespace SNAPME.Tokens
{
    public class UserDetailsToken
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }

        public AddressToken[] addresses { get; set; }
    }

    public static class UserDetailsTokenExtensions
    {
        public static UserDetailsToken AsToken(this UserDetails details)
        {
            return new UserDetailsToken 
            {
                id = details.id,
                first_name = details.first_name,
                middle_name = details.middle_name,
                last_name = details.last_name,
                email = details.email,
                gender = details.gender,
                addresses = details.addresses.Select(a => a.AsToken()).ToArray()
            };
        }

        public static UserDetails AsUserDetails(this UserDetailsToken details)
        {
            return new UserDetails
            {
                id = details.id,
                first_name = details.first_name,
                middle_name = details.middle_name,
                last_name = details.last_name,
                email = details.email,
                gender = details.gender,
                addresses = details.addresses != null ? details.addresses.Select(a => a.AsAddress()).ToArray() : new Address[0]
            };
        }
    }
}
