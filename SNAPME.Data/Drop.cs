using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class Drop
    {
        public DateTime date { get; set; }
        public string user_id { get; set; }
        public int current_price { get; set; }
        public bool backed_out { get; set; }
        public bool checked_out { get; set; }
    }
}
