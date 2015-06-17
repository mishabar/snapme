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
    public class FriendRepository : IFriendRepository
    {
        private MongoCollection<Friend> _collection;

        public FriendRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<Friend>("friends");
        }

        public void StoreFriends(long me, string name, string iisnap_id, Dictionary<long, string> friends)
        {
            var friend = _collection.FindAndModify(new FindAndModifyArgs
            {
                Query = Query.And(Query<Friend>.EQ(f => f.id, me), Query<Friend>.EQ(f => f.iisnap_id, iisnap_id)),
                Update = Update<Friend>.SetOnInsert(f => f.friends, new Dictionary<long, string>()).SetOnInsert(f => f.name, name),
                Upsert = true,
                VersionReturned = FindAndModifyDocumentVersion.Modified
            }).GetModifiedDocumentAs<Friend>();

            foreach (var item in friends)
            {
                if (!friend.friends.ContainsKey(item.Key))
                {
                    friend.friends.Add(item.Key, item.Value);
                    EnsureFriendship(item.Key, me);
                }
            }

            _collection.Save<Friend>(friend);
        }

        private void EnsureFriendship(long me, long self)
        {
            var friend = _collection.FindAndModify(new FindAndModifyArgs
            {
                Query = Query<Friend>.EQ(f => f.id, me),
                Update = Update<Friend>.SetOnInsert(f => f.friends, new Dictionary<long, string>()),
                Upsert = true,
                VersionReturned = FindAndModifyDocumentVersion.Modified
            }).GetModifiedDocumentAs<Friend>();

            if (!friend.friends.ContainsKey(self))
            {
                var user = _collection.FindOne(Query<Friend>.EQ(f => f.id, self));
                if (user != null)
                {
                    friend.friends.Add(self, user.name);
                    _collection.Save<Friend>(friend);
                }
            }
        }
    }
}
