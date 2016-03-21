using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Data.Entities
{
    public class Sequence
    {
        public static readonly string CollectionName = "id_sequences";

        [BsonId, BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string id { get; set; }
        public long value { get; set; }
    }
}
