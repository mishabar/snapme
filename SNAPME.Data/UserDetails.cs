using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class UserDetails
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }

        public Address[] addresses { get; set; }

        public static UserDetails Empty = new UserDetails { addresses = new Address[0] };
    }
}
