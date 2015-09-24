using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class Sale
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public int current_price { get; set; }
        public int target_price { get; set; }
        public int progress { get; set; }
        public Dictionary<string, Drop> drops { get; set; }
        public DateTime? started_on { get; set; }
        public DateTime ends_on { get; set; }
        public DateTime? ended_on { get; set; }
        public bool active { get; set; }
        public int potential_points { get; set; }
        public bool is_featured { get; set; }
        public string banner_image { get; set; }
        public string product_image { get; set; }
    }
}
