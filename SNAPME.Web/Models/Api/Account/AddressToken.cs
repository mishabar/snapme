using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Models.Api.Account
{
    public class AddressToken
    {
        public int idx { get; set; }
        public string name { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public int zip_code { get; set; }
    }
}