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
        public static readonly string CollectionName = "products";

        [BsonId(IdGenerator=typeof(NumberSequenceIdGenerator)), BsonRepresentation(MongoDB.Bson.BsonType.Int64)]
        public long id { get; set; }
        public long community_id { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public string full_details { get; set; }
        public double price { get; set; }
        public string images_mode { get; set; }
        public string[] images { get; set; }

        public SaleDetails sale { get; set; }
            
        public ShippingInfo shipping_info { get; set; }

        public ObjectState state { get; set; }
    }

    public class ShippingInfo
    {
        public string from_zip { get; set; }
        public int width { get; set; }
        public int length { get; set; }
        public int height { get; set; }
        public double weight { get; set; }
    }

    public class SaleDetails
    {
        public double target { get; set; }
        public double current_price { get; set; }
        public int progress { get; set; }
        public Snap[] snaps { get; set; }
        public int snaps_count { get; set; }
    }

    public class Snap
    {
        public string user_id { get; set; }
        public string external_user_id { get; set; }
        public DateTime snapped_at { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public string charge_id { get; set; }
        public int product_amount { get; set; }
        public int shipping_amount { get; set; }
    }
}
