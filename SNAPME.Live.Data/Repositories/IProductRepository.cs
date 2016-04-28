using SNAPME.Live.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetCommunityProducts(long[] communityIds);

        Task<Product> GetProduct(string name);

        Task<Product> GetProduct(long id);

        Task<Snap> Snap(long productId, Snap snap);

        Task<IEnumerable<Product>> GetProducts();

        Task<bool> UpdateProduct(Product product);

        Task<IEnumerable<SaleDetails>> GetSales(long id);

        Task<bool> SaveSale(long product_id, SaleDetails saleDetails);
    }
}
