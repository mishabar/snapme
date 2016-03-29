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
    }
}
