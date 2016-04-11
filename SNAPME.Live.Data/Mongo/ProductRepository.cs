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
            var projection = Builders<Product>.Projection;

            var list = await (await collection.FindAsync(filter.Eq(p => p.name, name), new FindOptions<Product, Product> { Projection = projection.Exclude(p => p.old_sales).Exclude(p => p.future_sales) })).ToListAsync();

            return list.FirstOrDefault();
        }


        public async Task<Product> GetProduct(long productId)
        {
            var collection = _db.GetCollection<Product>(Product.CollectionName);
            var filter = Builders<Product>.Filter;
            var projection = Builders<Product>.Projection;

            var list = await (await collection.FindAsync(filter.Eq(p => p.id, productId), new FindOptions<Product, Product> { Projection = projection.Exclude(p => p.old_sales).Exclude(p => p.future_sales) })).ToListAsync();

            return list.FirstOrDefault();
        }


        public async Task<Snap> Snap(long productId, Snap snap)
        {
            var product = await GetProduct(productId);
            if (product == null)
            {
                return null;
            }
            else
            {
                var sale = product.sale;


                // Do relevant checks in regard to sale state

                // Add snap object if needed
                var filter = Builders<Product>.Filter;
                var update = Builders<Product>.Update;
                var projection = Builders<Product>.Projection;

                var collection = _db.GetCollection<Product>(Product.CollectionName);
                collection.FindOneAndUpdate(filter.And(
                    filter.Eq(p => p.id, productId), filter.Eq(p => p.sale.sale_id, sale.sale_id)),
                    update.Push(p => p.sale.snaps, snap).Inc(p => p.sale.snaps_count, 1),
                    new FindOneAndUpdateOptions<Product> { IsUpsert = false, ReturnDocument = ReturnDocument.After, Projection = projection.Include(p => p.id) });

                return snap;
            }
        }


        public async Task<IEnumerable<Product>> GetProducts()
        {
            var collection = _db.GetCollection<Product>(Product.CollectionName);
            return await (await collection.FindAsync(Builders<Product>.Filter.Empty)).ToListAsync();
        }


        public async Task<bool> UpdateProduct(Product product)
        {
            var collection = _db.GetCollection<Product>(Product.CollectionName);
            var filter = Builders<Product>.Filter;
            var update = Builders<Product>.Update;
            var projection = Builders<Product>.Projection;

            await collection.FindOneAndUpdateAsync(
                filter.Eq(p => p.id, product.id),
                update.Set(p => p.name, product.name).Set(p => p.description, product.description)
                      .Set(p => p.full_details, product.full_details).Set(p => p.images_mode, product.images_mode)
                      .Set(p => p.images, product.images).Set(p => p.shipping_info, product.shipping_info),
                new FindOneAndUpdateOptions<Product> { Projection = projection.Include(p => p.id) });

            return true;
        }
    }
}
