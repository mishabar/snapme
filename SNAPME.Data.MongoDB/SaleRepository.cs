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


        public IEnumerable<Sale> GetActive()
        {
            return _collection.Find(Query<Sale>.EQ(s => s.active, true));
        }

        public void Save(Sale sale)
        {
            _collection.FindAndModify(new FindAndModifyArgs
            {
                Query = Query<Sale>.EQ(s => s.id, sale.id),
                Update = Update<Sale>
                    .Set(s => s.product_id, sale.product_id)
                    .Set(s => s.name, sale.name)
                    .Set(s => s.summary, sale.summary)
                    .Set(s => s.retail_price, sale.retail_price)
                    .Set(s => s.target, sale.target)
                    .Set(s => s.price, sale.price)
                    .Set(s => s.progress, sale.progress)
                    .Set(s => s.is_featured, sale.is_featured)
                    .Set(s => s.points, sale.points)
                    .Set(s => s.starts_on, sale.starts_on)
                    .Set(s => s.duration, sale.duration)
            });
        }


        public void Create(Sale sale)
        {
            _collection.Save(sale);
        }


        public Sale GetScheduledSale(string productId)
        {
            return _collection.FindOne(Query.And(Query<Sale>.EQ(s => s.product_id, productId), Query<Sale>.EQ(s => s.ended, false)));
        }


        public void AdjustSalesStatus()
        {
            var sales = _collection.Find(Query<Sale>.EQ(s => s.ended, false)).ToArray();

            foreach (var sale in sales)
            {
                if (sale.active && sale.starts_on.AddHours(sale.duration) < DateTime.UtcNow)
                {
                    sale.ended = true;
                    sale.active = false;
                    _collection.Save(sale);
                }
                else if (!sale.active && sale.starts_on < DateTime.UtcNow)
                {
                    sale.active = true;
                    _collection.Save(sale);
                }
            }
        }


        public Sale JoinSale(string userId, string productId)
        {
            var sale = _collection.FindOne(Query.And(Query<Sale>.EQ(s => s.product_id, productId), Query<Sale>.EQ(s => s.ended, false)));

            if (sale != null && !sale.ended && sale.price > sale.target)
            {
                var additionalPercentage = 1F;
                if (sale.drops.Count() > 0)
                {
                    Random random = new Random((int)DateTime.UtcNow.Ticks);
                    additionalPercentage = Math.Abs(1F - (float)sale.quantity / (float)sale.drops.Count());
                    additionalPercentage = 1F + Math.Min((float)random.NextDouble(), additionalPercentage);
                }
                sale.price -= (int)Math.Floor((sale.retail_price - sale.target) / sale.quantity /*consider intoducing additional property*/ * additionalPercentage);
                if (sale.price < sale.target)
                {
                    sale.price = sale.target;
                }

                sale = _collection.FindAndModify(
                    new FindAndModifyArgs
                    {
                        Query = Query.And(Query<Sale>.EQ(s => s.product_id, productId), Query<Sale>.EQ(s => s.ended, false)),
                        Update = Update<Sale>
                            .Push<Drop>(s => s.drops, new Drop { current_price = sale.price, user_id = userId, date = DateTime.UtcNow, checked_out = false, backed_out = false })
                            .Set(s => s.price, sale.price)
                            .Set(s => s.progress, Math.Floor(((float)sale.retail_price - (float)sale.price) / ((float)sale.retail_price - (float)sale.target) * 100)),
                            //.Set(s => s.ended, sale.price == sale.target),
                        Upsert = false,
                        VersionReturned = FindAndModifyDocumentVersion.Modified
                    }).GetModifiedDocumentAs<Sale>();
            }

            return sale;
        }
    }
}
 