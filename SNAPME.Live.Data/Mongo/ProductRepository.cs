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
    public class ProductRepository : IProductRepository
    {
        private IMongoDatabase _db;

        public ProductRepository(IMongoDatabase db)
        {
            _db = db;
        }


        public async Task<IEnumerable<Entities.Product>> GetCommunityProducts(long[] communityIds)
        {
            var collection = _db.GetCollection<Product>(Product.CollectionName);
            var filter = Builders<Product>.Filter;
            return await (await collection.FindAsync(filter.And(filter.Eq(p => p.state, ObjectState.Visible), filter.In(p => p.community_id, communityIds)))).ToListAsync();
        }


        public async Task<Product> GetProduct(string name)
        {
            var collection = _db.GetCollection<Product>(Product.CollectionName);
            var filter = Builders<Product>.Filter;

            var list = await (await collection.FindAsync(filter.Eq(p => p.name, name))).ToListAsync();

            return list.FirstOrDefault();
        }
    }
}
