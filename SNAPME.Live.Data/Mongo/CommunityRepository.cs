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
    public class CommunityRepository : ICommunityRepository
    {
        private IMongoDatabase _db;

        public CommunityRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Community>> ListCommunities()
        {
            return await (await _db.GetCollection<Community>(Community.CollectionName)
                            .FindAsync(Builders<Community>.Filter.Eq(c => c.state, ObjectState.Visible))).ToListAsync();
        }

        public async Task<Community> GetCommunity(string name)
        {
            return (await (await _db.GetCollection<Community>(Community.CollectionName)
                        .FindAsync(Builders<Community>.Filter.Eq(c => c.name, name), new FindOptions<Community> { Limit = 1 })).ToListAsync()).FirstOrDefault();
        }
    }
}
