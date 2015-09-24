using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllForSeller(string id);

        Product GetById(string id);

        void Save(Product product);

        void SaveImage(string id, string image, int idx);

        void AddLike(string productId, string userId, bool add);

        IEnumerable<Product> GetByIds(string[] ids);

        IEnumerable<Product> GetAll();

        void CheckProduct(string id);

        string GetImageById(string id, int idx);

        IEnumerable<Product> GetFiltered(string query, int page, out bool hasData);
    }
}
