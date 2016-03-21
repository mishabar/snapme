using MongoDB.Bson.Serialization.Attributes;
using SNAPME.Live.Data.Mongo.IDGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Data.Entities
{
    public class Community
    {
        [BsonId(IdGenerator=typeof(NumberSequenceIdGenerator)), BsonRepresentation(MongoDB.Bson.BsonType.Int64)]
        public long id { get; set; }

        public string name { get; set; }
        public string icon { get; set; }
        public CommunityBanner banner { get; set; }
    }

    public class CommunityBanner 
    {
        public string nameClass { get; set; }
        public string url { get; set; }
        public string position { get; set; }
    }
}
