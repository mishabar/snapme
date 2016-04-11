using SNAPME.Live.Data.Entities;
using SNAPME.Live.Data.Repositories;
using SNAPME.Live.Services.Interfaces;
using SNAPME.Live.Services.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Live.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<SimpleProductToken>> GetCommunityProducts(long[] communityIds)
        {
            return (await _productRepository.GetCommunityProducts(communityIds)).Select(p => new SimpleProductToken(p));
        }


        public async Task<FullProductToken> GetProduct(string name)
        {
            var product = await _productRepository.GetProduct(name);
            return product == null ? null : new FullProductToken(product);
        }


        public async Task<FullProductToken> GetProduct(long productId)
        {
            var product = await _productRepository.GetProduct(productId);
            return product == null ? null : new FullProductToken(product);
        }


        public async Task<bool> UpdateProduct(FullProductToken request)
        {
            return await _productRepository.UpdateProduct(request.AsProduct());
        }


        public async Task<SnapToken> JoinSale(long productId, string userId, string userName, string customerId, string chargeId, 
            string shippingFirstName, string shippingLastName, string shippingAddress, string shippingAddress2, string shippingCity, 
            string shippingState, string shippingZip)
        {
            return null;
            //return (await _productRepository.Snap(productId, new Snap
            //{
            //    external_user_id = customerId,
            //    user_id = userId,
            //    name = userName,
            //    charge_id = chargeId,
            //    quantity = 0,
            //    product_amount = 0,
            //    shipping_amount = 0,
            //    snapped_at = DateTime.Now,
            //    address = new DestinationShippingInfo { 
            //        first_name = shippingFirstName, 
            //        last_name = shippingLastName, 
            //        address = shippingAddress,
            //        address2 = shippingAddress2,
            //        city = shippingCity,
            //        state = shippingState,
            //        zip = shippingZip
            //    }
            //})).AsToken();
        }


        public async Task<IEnumerable<BaseProductToken>> GetProducts()
        {
            return (await _productRepository.GetProducts()).Select(p => new BaseProductToken(p));
        }
    }
}
