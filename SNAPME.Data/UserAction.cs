using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class UserAction
    {
        public static readonly int LIKED = 0;
        public static readonly int FAVORED = 1;
        public static readonly int JOINED_SALE = 2;
        public static readonly int BOUGHT = 3;

        public string id { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public int action { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public DateTime ts { get; set; }
    }
}
