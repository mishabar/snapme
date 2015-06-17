using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNAPME.Web.Helpers
{
    public static class Extensions
    {
        public static string ToProductId(this string id)
        {
            return id.Split('-').Last();
        }
    }
}