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
    public class SellerRepository : ISellerRepository
    {
        private MongoCollection<Seller> _colletion;

        public SellerRepository(MongoDatabase db)
        {
            _colletion = db.GetCollection<Seller>("sellers");
        }

        public Seller Save(Seller seller)
        {
            _colletion.Save(seller);

            return seller;
        }


        public IEnumerable<Seller> GetAll()
        {
            return _colletion.FindAll();
        }


        public Seller GetById(string id)
        {
            return _colletion.FindOne(Query<Seller>.EQ(q => q.id, id));
        }


        public void Archive(string id)
        {
            _colletion.Update(Query<Seller>.EQ(s => s.id, id), Update<Seller>.Set(s => s.is_archived, true));
        }
    }
}
