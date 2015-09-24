using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class Comment
    {
        public string user_id { get; set; }
        public string username { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }
    }
}
