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
    public class ProductRepository : IProductRepository
    {
        private MongoCollection<Product> _collection;

        public ProductRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<Product>("products");
        }

        public IEnumerable<Product> GetAllForSeller(string id)
        {
            return _collection.Find(Query<Product>.EQ(p => p.seller_id, id)).ToArray();
        }


        public Product GetById(string id)
        {
            return _collection.FindOne(Query<Product>.EQ(p => p.id, id));
        }
    }
}
