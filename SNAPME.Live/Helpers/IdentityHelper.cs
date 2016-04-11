using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Net.Http;

namespace SNAPME.Live.Helpers
{
    public static class IdentityHelper
    {
        public static string GetImageUrl(this IIdentity identity)
        {
            if (identity is ClaimsIdentity)
            {
                return (identity as ClaimsIdentity).FindFirst("ii-image").Value;
            }

            return string.Empty;
        }

        public static string GetFacebookId(this IIdentity identity)
        {
            if (identity is ClaimsIdentity)
            {
                return (identity as ClaimsIdentity).FindFirst("fb-id").Value;
            }

            return string.Empty;
        }
    }
}