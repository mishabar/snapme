using MongoDB.Bson.Serialization.Attributes;
using SNAPME.Live.Data.Mongo.IDGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Data.Entities
{
    public class Product
    {
        [BsonId(IdGenerator=typeof(NumberSequenceIdGenerator)), BsonRepresentation(MongoDB.Bson.BsonType.Int64)]
        public long id { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public string full_details { get; set; }
        public double price { get; set; }
        public double target { get; set; }


        public ShippingInfo shipping_info { get; set; }
    }

    public class ShippingInfo
    {
        public string from_zip { get; set; }
        public int width { get; set; }
        public int length { get; set; }
        public int height { get; set; }
        public double weight { get; set; }
    }
}
