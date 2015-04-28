using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Tokens;
using SNAPME.Tokens.Admin;

namespace SNAPME.Services.Interfaces
{
    public interface ISellerService
    {
        SellerToken Save(SellerToken token);

        IEnumerable<SellerToken> GetAll();

        SellerToken GetById(string id);

        IEnumerable<ProductToken> GetAllProducts(string id);

        ProductToken GetProduct(string id);

        void SaveProduct(ProductToken product);

        void SaveProductImage(string id, string image, int idx);

        void Archive(string id);

        void CheckProduct(string id);
    }
}
