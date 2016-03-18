using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Data.Entities
{
    public class BetaRegistration
    {
        public static readonly string CollectionName = "beta_registrations";

        [BsonId, BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string email { get; set; }
        public DateTime date { get; set; }
        public bool invitation_sent { get; set; }
    }
}
