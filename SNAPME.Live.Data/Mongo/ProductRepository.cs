using MongoDB.Bson;
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
            return await (await collection.FindAsync(filter.And(filter.Eq(p => p.state, ObjectState.Visible), filter.Ne(p => p.sale, null), filter.In(p => p.community_id, communityIds)))).ToListAsync();
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


        public async Task<IEnumerable<SaleDetails>> GetSales(long id)
        {
            var collection = _db.GetCollection<Product>(Product.CollectionName);
            var filter = Builders<Product>.Filter;
            var projection = Builders<Product>.Projection;

            var products = await (await collection.FindAsync(filter.Eq(p => p.id, id),
                new FindOptions<Product> { Limit = 1, Projection = projection.Include(p => p.sale).Include(p => p.old_sales).Include(p => p.future_sales) })).ToListAsync();

            if (products.Any())
            {
                List<SaleDetails> sales = new List<SaleDetails>();
                var product = products.FirstOrDefault();
                sales.AddRange(product.old_sales ?? Enumerable.Empty<SaleDetails>());
                sales.AddRange(product.future_sales ?? Enumerable.Empty<SaleDetails>());
                if (product.sale != null)
                {
                    sales.Add(product.sale);
                }

                return sales;
            }

            return Enumerable.Empty<SaleDetails>();
        }


        public async Task<bool> SaveSale(long product_id, SaleDetails saleDetails)
        {
            try
            {
                bool isNew = false;
                if (saleDetails.sale_id == 0)
                {
                    isNew = true;
                    // get next sale id
                    var seqCollection = _db.GetCollection<BsonDocument>("sequences");
                    var sequences = await seqCollection.FindOneAndUpdateAsync(Builders<BsonDocument>.Filter.Empty, Builders<BsonDocument>.Update.Inc(new StringFieldDefinition<BsonDocument, long>("sale_id"), 1), new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = false, ReturnDocument = ReturnDocument.After });
                    saleDetails.sale_id = sequences.GetValue("sale_id").AsInt64;
                    saleDetails.snaps = new Snap[0];
                    saleDetails.progress = 0;
                }

                var collection = _db.GetCollection<Product>(Product.CollectionName);
                var filter = Builders<Product>.Filter;
                var update = Builders<Product>.Update;
                var projection = Builders<Product>.Projection;

                if (isNew)
                {
                    await collection.UpdateOneAsync(filter.Eq(p => p.id, product_id), update.Push(p => p.future_sales, saleDetails));
                }
                else
                {
                    var product = await (await collection.FindAsync(filter.Eq(p => p.id, product_id), new FindOptions<Product> { Limit = 1 })).FirstOrDefaultAsync();
                    var sale = product.future_sales.FirstOrDefault(s => s.sale_id == saleDetails.sale_id);
                    sale.starts_on = saleDetails.starts_on;
                    sale.ends_on = saleDetails.ends_on;
                    sale.sale_type = saleDetails.sale_type;
                    sale.stock = saleDetails.stock;
                    sale.required_snaps = saleDetails.required_snaps;
                    sale.target = saleDetails.target;

                    await collection.UpdateOneAsync(filter.Eq(p => p.id, product_id), update.Set(p => p.future_sales, product.future_sales));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
