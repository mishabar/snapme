using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
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


        public void Create(Product product)
        {
            _collection.Save(product);
        }


        public void Save(Product product)
        {
            _collection.FindAndModify(new FindAndModifyArgs
            {
                Query = Query<Product>.EQ(p => p.id, product.id),
                Update = Update<Product>
                        .Set(p => p.seller_id, product.seller_id)
                        .Set(p => p.category, product.category)
                        .Set(p => p.name, product.name)
                        .Set(p => p.sku, product.sku)
                        .Set(p => p.description, product.description)
                        .Set(p => p.short_description, product.short_description)
                        .Set(p => p.retail_price, product.retail_price)
                        .Set(p => p.size, product.size)
                        .Set(p => p.weight, product.weight)
                        .SetOnInsert(p => p.images, new string[6])
                        .SetOnInsert(p => p.likes, new string[0])
                        .SetOnInsert(p => p.is_draft, true),
                Upsert = false
            });
        }


        public void SaveImage(string id, string image, int idx)
        {
            if (idx > 6) return;

            var product = _collection.FindAndModify(new FindAndModifyArgs
            {
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


        public void AddLike(string productId, string userId, bool add)
        {
            if (add)
            {
                _collection.Update(Query<Product>.EQ(p => p.id, productId),
                    Update<Product>.Push(p => p.likes, userId).Inc(p => p.likes_count, 1));
            }
            else
            {
                _collection.Update(Query<Product>.EQ(p => p.id, productId),
                    Update<Product>.Pull(p => p.likes, userId).Inc(p => p.likes_count, -1));
            }
        }


        public IEnumerable<Product> GetByIds(string[] ids)
        {
            return _collection.Find(Query<Product>.In(p => p.id, ids));
        }


        public IEnumerable<Product> GetAll()
        {
            return _collection.FindAll();
        }


        public void CheckProduct(string id)
        {
            var product = _collection.FindOne(Query<Product>.EQ(p => p.id, id));

            if (product != null && product.size == null)
            {
                _collection.Remove(Query<Product>.EQ(p => p.id, id));
            }
        }


        public string GetImageById(string id, int idx)
        {
            return _collection.Find(Query<Product>.EQ(p => p.id, id)).SetFields(Fields<Product>.Slice(p => p.images, idx, 1)).First().images[0];
        }


        public IEnumerable<Product> GetFiltered(string query, int page, out bool hasData)
        {
            var queries = new List<IMongoQuery>();
            queries.Add(Query<Product>.Exists(p => p.id));
            if (!string.IsNullOrWhiteSpace(query))
            {
                queries.Add(Query<Product>.Matches(p => p.name, new BsonRegularExpression(query, "i")));
            }

            var products = _collection.Find(Query.And(queries))/*.SetSkip(200 * (page - 1)).SetLimit(200)*/.ToArray();
            hasData = products.Length > 0;

            return products;
        }


        public Comment AddComment(string id, Comment comment)
        {
            _collection.FindAndModify(new FindAndModifyArgs
            {
                Query = Query<Product>.EQ(p => p.id, id),
                Update = Update.PushEachWrapped<Comment>("comments", new PushEachOptions { Position = 0 }, new Comment[] { comment }),
                Fields = Fields<Product>.Include(p => p.id)
            });

            return comment;
        }
    }
}
