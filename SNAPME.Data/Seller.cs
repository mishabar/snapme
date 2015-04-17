using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class Seller
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string business_type { get; set; }
        public string legal_name { get; set; }
        public string trading_name { get; set; }
        public string tax_id { get; set; }
        public string address { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string pickup_address { get; set; }
        public string comments { get; set; }
        public bool is_archived { get; set; }
    }
}
