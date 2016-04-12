using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Tokens
{
    public class SnapToken
    {
        public string user_id { get; set; }
        public string name { get; set; }

        public SnapToken(Snap snap)
        {
            user_id = snap.user_id;
            name = snap.name;
        }
    }
}
