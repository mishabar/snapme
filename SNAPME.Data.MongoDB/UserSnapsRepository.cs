using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SNAPME.Data.Repositories;

namespace SNAPME.Data.MongoDB
{
    public class UserSnapsRepository : IUserSnapsRepository
    {
        private MongoCollection<UserSnap> _collection;

        public UserSnapsRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<UserSnap>("user_snaps");
        }

        public IEnumerable<UserSnap> GetSnaps(string id)
        {
            return _collection.Find(Query<UserSnap>.EQ(s => s.user_id, "demo")).SetSortOrder(SortBy<UserSnap>.Descending(s => s.snapped_at));
        }
    }
}
