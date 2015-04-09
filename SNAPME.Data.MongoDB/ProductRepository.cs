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


        public void Save(Product product)
        {
            _collection.FindAndModify(new FindAndModifyArgs {
                Query = Query<Product>.EQ(p => p.id, product.id),
                Update = Update<Product>
                        .Set(p => p.seller_id, product.seller_id)
                        .Set(p => p.category, product.category)
                        .Set(p => p.name, product.name)
                        .Set(p => p.descritpion, product.descritpion)
                        .Set(p => p.short_descritpion, product.short_descritpion)
                        .Set(p => p.condition, product.condition)
                        .Set(p => p.retail_price, product.retail_price)
                        .Set(p => p.purchase_price, product.purchase_price)
                        .Set(p => p.size, product.size)
                        .Set(p => p.weight, product.weight)
                        .SetOnInsert(p => p.images, new string[6])
                        .SetOnInsert(p => p.is_draft, true),
                Upsert = true
            });
        }


        public void SaveImage(string id, string image, int idx)
        {
            if (idx >= 6) return;

            var product = _collection.FindAndModify(new FindAndModifyArgs {
                Query = Query<Product>.EQ(p => p.id, id),
                Update = Update.Set(string.Format("images.{0}", idx), image),
                Upsert = false,
                VersionReturned = FindAndModifyDocumentVersion.Modified
            }).GetModifiedDocumentAs<Product>();

            if (product == null) 
            {
                product = new Product { id = id, images = new string[6], is_draft = true };
                product.images[idx] = image;
                _collection.Save(product);
            }
        }
    }
}
