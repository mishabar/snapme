using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data
{
    public class UserPreferences
    {
        public string id { get; set; }
        public string[] likes { get; set; }
        public string[] favorites { get; set; }

        public UserPreferences(string userId)
        {
            id = userId;
            likes = new string[0];
            favorites = new string[0];
        }
    }
}
