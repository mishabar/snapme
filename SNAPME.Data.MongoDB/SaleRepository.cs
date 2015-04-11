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
    public class SaleRepository : ISaleRepository
    {
        private MongoCollection<Sale> _collection;

        public SaleRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<Sale>("sales");
        }

        public Sale GetById(string id, bool active)
        {
            return _collection.FindOne(Query.And(Query<Sale>.EQ(s => s.product_id, id), Query<Sale>.EQ(s => s.active, true)));
        }
    }
}
