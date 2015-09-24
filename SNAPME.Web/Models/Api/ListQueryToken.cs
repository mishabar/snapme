using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Models.Api
{
    public class ListQueryToken
    {
        public string query { get; set; }
        public int page { get; set; }
    }
}