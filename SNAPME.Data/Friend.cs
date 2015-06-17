using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class Friend
    {
        public long id { get; set; }
        public string name { get; set; }
        public string iisnap_id { get; set; }
        public Dictionary<long, string> friends { get; set; }
    }
}
