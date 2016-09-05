using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Data.Entities
{
    public class Address
    {
        public bool primary { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    }

    public class UserSettings
    {
        [BsonId]
        public string id { get; set; }
        public Address[] addresses { get; set; }
        public long[] communities { get; set; }
    }
}
