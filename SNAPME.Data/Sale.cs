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
        public string name { get; set; }
        public string summary { get; set; }
        public int retail_price { get; set; }
        public int target { get; set; }
        public int price { get; set; }
        public IEnumerable<Drop> drops { get; set; }
        public int quantity { get; set; }
        public int progress { get; set; }
        public bool is_featured { get; set; }
        public int points { get; set; }

        public DateTime starts_on { get; set; }
        public int duration { get; set; }
        public bool active { get; set; }
        public bool ended { get; set; }
    }
}
