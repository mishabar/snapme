using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class ProductActivity
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public IEnumerable<Activity> activities { get; set; }
    }

    public class Activity
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public ActivityType type { get; set; }
        public DateTime created_on { get; set; }
        public Dictionary<string, object> data { get; set; }
    }

    public enum ActivityType
    {
        Like,
        Favorite,
        Comment,
        JoinSale
    }
}
