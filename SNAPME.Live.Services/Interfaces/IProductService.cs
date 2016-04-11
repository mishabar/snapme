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
        Task<bool> UpdateProduct(FullProductToken request);

        Task<SnapToken> JoinSale(long productId, string userId, string useName, string customerId, string chargeId,
            string shippingFirstName, string shippingLastName, string shippingAddress, string shippingAddress2,
            string shippingCity, string shippingState, string shippingZip);

        Task<IEnumerable<BaseProductToken>> GetProducts();
    }
}
