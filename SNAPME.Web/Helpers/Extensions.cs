using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace SNAPME.Web.Helpers
{
    public static class Extensions
    {
        public static string ToProductId(this string id)
        {
            return id.Split('-').Last();
        }

        public static string DisplayName(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirst("urn:iisnap:name").Value;
        }

        public static string ProfileImage(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirst("urn:iisname:image_url").Value;
        }
    }
}