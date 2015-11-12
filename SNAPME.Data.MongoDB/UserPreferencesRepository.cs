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
    public class UserPreferencesRepository : IUserPreferencesRepository
    {
        private MongoCollection<UserPreferences> _collection;
        private MongoCollection<Product> _productsCollection;

        public UserPreferencesRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<UserPreferences>("user_preferences");
            _productsCollection = db.GetCollection<Product>("products");
        }

        public bool Likes(string userId, string productId)
        {
            var preferences = _collection.FindOne(Query<UserPreferences>.EQ(u => u.id, userId));
            if (preferences == null)
            {
                preferences = new UserPreferences(userId);
                preferences.likes = new[] { productId };
                _collection.Save(preferences);

                return true;
            }

            if (preferences.likes.Contains(productId))
            {
                _collection.Update(Query<UserPreferences>.EQ(u => u.id, userId),
                    Update<UserPreferences>.Pull(u => u.likes, productId));
                return false;
            }
            else
            {
                _collection.Update(Query<UserPreferences>.EQ(u => u.id, userId),
                    Update<UserPreferences>.Push(u => u.likes, productId));
                return true;
            }
        }

        public bool Favors(string userId, string productId)
        {
            var preferences = _collection.FindOne(Query<UserPreferences>.EQ(u => u.id, userId));
            if (preferences == null)
            {
                preferences = new UserPreferences(userId);
                preferences.favorites = new[] { productId };
                _collection.Save(preferences);

                return true;
            }

            if (preferences.favorites.Contains(productId))
            {
                _collection.Update(Query<UserPreferences>.EQ(u => u.id, userId),
                    Update<UserPreferences>.Pull(u => u.favorites, productId));
                return false;
            }
            else
            {
                _collection.Update(Query<UserPreferences>.EQ(u => u.id, userId),
                    Update<UserPreferences>.Push(u => u.favorites, productId));
                return true;
            }
        }


        public UserPreferences GetById(string userId, string id)
        {
            var prefs = _collection.FindOne(Query<UserPreferences>.EQ(u => u.id, userId));
            return prefs ?? new UserPreferences(userId);
        }


        public Dictionary<string, string> AllFavorites(string id)
        {
            var prefs = _collection.FindOne(Query<UserPreferences>.EQ(u => u.id, id));
            if (prefs.favorites != null && prefs.favorites.Length > 0)
            {
                return _productsCollection.Find(Query<Product>.In(p => p.id, prefs.favorites)).SetFields(Fields<Product>.Include(p => p.name)).ToDictionary(p => p.id, p => p.name);
            }

            return new Dictionary<string, string>();
        }
    }
}
