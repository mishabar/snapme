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
        public static readonly string CollectionName = "communities";

        [BsonId(IdGenerator=typeof(NumberSequenceIdGenerator)), BsonRepresentation(MongoDB.Bson.BsonType.Int64)]
        public long id { get; set; }

        public string name { get; set; }
        public string icon { get; set; }
        public CommunityBanner banner { get; set; }

        public FeaturedProduct featured_product { get; set; }
        public FeaturedProduct[] featured_products { get; set; }

        public ObjectState state { get; set; }

        public Community()
        {
            state = ObjectState.Hidden;
        }
    }

    public class CommunityBanner 
    {
        public string nameClass { get; set; }
        public string url { get; set; }
        public string position { get; set; }
    }

    public class FeaturedProduct
    {
        public long product_id { get; set; }
        public DateTime featured_since { get; set; }
        public string video_url { get; set; }
        public string review { get; set; }
    }
}
