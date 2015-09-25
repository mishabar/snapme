using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class Product
    {
        public string id { get; set; }
        public string sku { get; set; }
        public string seller_id { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string short_descritpion { get; set; }
        public string descritpion { get; set; }
        public int retail_price { get; set; }
        public string[] images { get; set; }
        public string size { get; set; }
        public int weight { get; set; }
        public bool is_draft { get; set; }
        public IEnumerable<Comment> comments { get; set; }

        public string[] likes { get; set; }
        public long likes_count { get; set; }

        public UserAction[] feed { get; set; }

        public string sale_id { get; set; }
    }
}
