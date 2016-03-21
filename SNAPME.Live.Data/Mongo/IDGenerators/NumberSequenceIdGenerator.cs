using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Data.Mongo.IDGenerators
{
    public class NumberSequenceIdGenerator : IIdGenerator
    {
        private IMongoDatabase _db;

        public NumberSequenceIdGenerator()
        {
            _db = (IMongoDatabase)System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IMongoDatabase));
        }

        public object GenerateId(object container, object document)
        {
            string sequenceName = document.GetType().Name.ToLower() + "_id";

            var sequence = _db.GetCollection<Sequence>(Sequence.CollectionName).FindOneAndUpdateAsync(
                Builders<Sequence>.Filter.Eq(s => s.id, sequenceName),
                Builders<Sequence>.Update.Inc(s => s.value, 1),
                new FindOneAndUpdateOptions<Sequence> { ReturnDocument = ReturnDocument.After, IsUpsert = true });

            return sequence.Result.value;
        }

        public bool IsEmpty(object id)
        {
            return ((id is long) && (long)id == 0);
        }
    }
}
