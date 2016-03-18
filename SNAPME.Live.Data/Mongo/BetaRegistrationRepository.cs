using MongoDB.Driver;
using SNAPME.Live.Data.Entities;
using SNAPME.Live.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Data.Mongo
{
    public class BetaRegistrationRepository : IBetaRegistrationRepository
    {
        private IMongoDatabase _db;

        public BetaRegistrationRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<int> Register(string email)
        {
            var collection = _db.GetCollection<BetaRegistration>(BetaRegistration.CollectionName);
            try
            {
                await collection.InsertOneAsync(new BetaRegistration { email = email.ToLowerInvariant(), date = DateTime.UtcNow, invitation_sent = false });
                return 1;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate key error"))
                    return 2;
            }

            return 0;
        }
    }
}
