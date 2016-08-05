using SNAPME.Live.Services.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<SimpleProductToken>> GetCommunityProducts(long[] communityIds);

        Task<FullProductToken> GetProduct(string name);
        Task<FullProductToken> GetProduct(long productId);
        Task<long> SaveProduct(FullProductToken fullProductToken);
        Task<bool> UpdateProduct(FullProductToken fullProductToken);

        Task<SnapToken> JoinSale(long productId, string userId, string imageUrl, string useName, string customerId, string chargeId,
            string shippingFirstName, string shippingLastName, string shippingAddress, string shippingAddress2,
            string shippingCity, string shippingState, string shippingZip);

        Task<IEnumerable<BaseProductToken>> GetProducts();

        Task<IEnumerable<SaleToken>> GetSales(long id);

        Task<bool> SaveSale(long product_id, SaleToken saleToken);
    }
}
