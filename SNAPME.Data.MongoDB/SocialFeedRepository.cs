using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SNAPME.Data.Repositories;

namespace SNAPME.Data.MongoDB
{
    public class SocialFeedRepository : ISocialFeedRepository
    {
        private MongoCollection<UserAction> _collection;
        private MongoCollection<Product> _productsCollection;
        private MongoCollection<Friend> _friendsCollection;
        private MongoCollection _usersCollection;

        public SocialFeedRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<UserAction>("social_feed");
            _productsCollection = db.GetCollection<Product>("products");
            _friendsCollection = db.GetCollection<Friend>("friends");
            _usersCollection = db.GetCollection("users");
        }

        public async void Add(string userId, string productId, int action)
        {
            // find user
            var user = await Task.Run(() => _usersCollection.FindOneAs<BsonDocument>(Query<IdentityUser>.EQ(u => u.Id, userId)));

            // find product
            var product = await Task.Run(() => _productsCollection.FindOne(Query<Product>.EQ(p => p.id, productId)));

            if (user != null && product != null)
            {
                _collection.Insert(new UserAction { 
                    action = action, 
                    user_id = userId, 
                    user_name = user["Claims"].AsBsonArray.First(c => c["Type"].AsString == "urn:iisnap:name")["Value"].AsString,
                    product_id = productId,
                    product_name = product.name,
                    ts = DateTime.UtcNow
                });
            }
        }


        public IEnumerable<UserAction> GetByProductId(string productId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return _collection.Find(Query<UserAction>.EQ(u => u.product_id, productId)).SetSortOrder(SortBy<UserAction>.Descending(u => u.ts));

            var friends = _friendsCollection.FindOne(Query<Friend>.EQ(f => f.iisnap_id, userId)).friends.Keys.ToArray();
            var iisnapIds = _friendsCollection.Find(Query<Friend>.In(f => f.id, friends)).Select(f => f.iisnap_id).ToArray();

            return _collection.Find(Query.And(Query<UserAction>.EQ(u => u.product_id, productId), Query<UserAction>.In(u => u.user_id, iisnapIds)));
        }
    }
}
