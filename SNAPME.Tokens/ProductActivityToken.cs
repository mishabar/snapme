using SNAPME.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Tokens
{
    public class ProductActivityToken
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public ActivityType type { get; set; }
        public string text { get; set; }
        public string created_on { get; set; }
        public Dictionary<string, object> data { get; set; }
    }

    public static class ProductActivityTokenExtensions
    {
        public static ProductActivityToken AsToken(this Activity activity)
        {
            return new ProductActivityToken 
            {
                user_id = activity.user_id,
                user_name = activity.user_name,
                type = activity.type,
                text = CreateActivityText(activity),
                created_on = GetPrettyDate(activity.created_on),
                data = activity.data
            };
        }

        private static string CreateActivityText(Activity activity)
        {
            switch (activity.type)
            {
                case ActivityType.Like:
                    return string.Format("{0} liked this product", activity.user_name);
                case ActivityType.Favorite:
                    return string.Format("{0} added this product to favourits list", activity.user_name);
                case ActivityType.Comment:
                    return string.Format("{0} added a comment", activity.user_name);
                case ActivityType.JoinSale:
                    return string.Format("{0} joined the sale", activity.user_name);
            }

            return string.Empty;
        }

        static string GetPrettyDate(DateTime d)
        {
            TimeSpan s = DateTime.UtcNow.Subtract(d);

            int dayDiff = (int)s.TotalDays;
            int secDiff = (int)s.TotalSeconds;

            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return null;
            }

            // 5.
            // Handle same-day times.
            if (dayDiff == 0)
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return "just now";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1 minute ago";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0} minutes ago",
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 hour ago";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0} hours ago",
                        Math.Floor((double)secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (dayDiff == 1)
            {
                return "yesterday";
            }
            if (dayDiff < 7)
            {
                return string.Format("{0} days ago",
                dayDiff);
            }
            if (dayDiff < 31)
            {
                return string.Format("{0} weeks ago",
                Math.Ceiling((double)dayDiff / 7));
            }
            
            return d.ToString("s");
        }
    }
}
